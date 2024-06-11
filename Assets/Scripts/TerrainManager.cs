using System;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    private TerrainData _terrainData;
    private int _alphamapWidth;
    private int _alphamapHeight;
    private int _numTextures;
    private float[,,] _splatmapData;
    
    private bool _touchingGround;
    private SurfaceType _surfaceType;
    private bool _inWater;

    private void Start()
    {
        GetTerrainProps();
    }

    private void FixedUpdate()
    {
        if (_touchingGround)
        {
            UpdateSurfaceByTerrain();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is TerrainCollider){
            Debug.LogWarning("Touching terrain");
            _touchingGround = true;
        } 
        else
        {
            SoundSurface surface = other.GetComponent<SoundSurface>();
            if (surface)
            {
                TrySetSurface(surface.Surface);
                if (surface.Surface == SurfaceType.Water)
                {
                    _inWater = true;
                    Debug.LogWarning("Enter water");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SoundSurface surface = other.GetComponent<SoundSurface>();
        if (surface)
        {
            TrySetSurface(surface.Surface);
            if (surface.Surface == SurfaceType.Water)
            {
                _inWater = false;
                Debug.LogWarning("Exit water");
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        OnTriggerEnter(other.collider);
    }

    private void UpdateSurfaceByTerrain()
    {
        int terrainNumber = GetTerrainAtPosition(transform.position);
        
        SurfaceType surfaceType = terrainNumber switch {
            0 => SurfaceType.Grass,
            1 => SurfaceType.Dirt,
            2 => SurfaceType.Stone,
            // 3 => SurfaceType.Water,
            // 4 => SurfaceType.Wood,
            _ => SurfaceType.Grass
        };
        
        TrySetSurface(surfaceType);
    }

    private void TrySetSurface(SurfaceType surfaceType)
    {
        if (_surfaceType == surfaceType)
            return;
        
        var str = surfaceType switch {
            SurfaceType.Grass => "grass",
            SurfaceType.Dirt => "grass",
            SurfaceType.Stone => "asphalt",
            SurfaceType.Asphalt => "asphalt",
            SurfaceType.Water => "water",
            SurfaceType.Wood => "wood",
            _ => "grass"
        };

        _surfaceType = surfaceType;
        AkSoundEngine.SetSwitch("surface_type", str, gameObject);
        Debug.LogWarning("Change surface to " + surfaceType);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider is TerrainCollider){
            Debug.LogWarning("Exit terrain");
            _touchingGround = false;
        }
    }

    public int GetTerrainAtPosition(Vector3 pos)
    {
        int terrainIdx = GetActiveTerrainTextureIdx(pos);
        return terrainIdx;
    }

    private void GetTerrainProps()
    {
        _terrainData = Terrain.activeTerrain.terrainData;
        _alphamapWidth = _terrainData.alphamapWidth;
        _alphamapHeight = _terrainData.alphamapHeight;

        _splatmapData = _terrainData.GetAlphamaps(0, 0, _alphamapWidth, _alphamapHeight);
        _numTextures = _splatmapData.Length / (_alphamapWidth * _alphamapHeight);
    }

    private Vector3 ConvertToSplatMapCoordinate(Vector3 playerPos)
    {
        Vector3 vecRet = new Vector3();
        Terrain ter = Terrain.activeTerrain;
        Vector3 terPosition = ter.transform.position;
        vecRet.x = ((playerPos.x - terPosition.x) / ter.terrainData.size.x) * ter.terrainData.alphamapWidth;
        vecRet.z = ((playerPos.z - terPosition.z) / ter.terrainData.size.z) * ter.terrainData.alphamapHeight;
        return vecRet;
    }

    private int GetActiveTerrainTextureIdx(Vector3 pos)
    {
        Vector3 terrainPos = ConvertToSplatMapCoordinate(pos);
        int ret = 0;
        float comp = 0f;
        for (int i = 0; i < _numTextures; i++)
        {
            if (comp < _splatmapData[(int)terrainPos.z, (int)terrainPos.x, i])
                ret = i;
        }

        return ret;
    }
}