using UnityEngine;

public class IgnoreMeshCollider : MonoBehaviour
{
    public string waterTag = "Water";  // ���ʃI�u�W�F�N�g�ɐݒ肷��^�O

    void Start()
    {
        // �^�O�Ő��ʃI�u�W�F�N�g��T��
        GameObject waterSurface = GameObject.FindGameObjectWithTag(waterTag);

        if (waterSurface != null)
        {
            MeshCollider waterMeshCollider = waterSurface.GetComponent<MeshCollider>();
            Collider objectCollider = GetComponent<Collider>();

            if (waterMeshCollider != null && objectCollider != null)
            {
                // �Փ˂𖳎�
                Physics.IgnoreCollision(objectCollider, waterMeshCollider);
                Debug.Log($"{gameObject.name} �� {waterSurface.name} �� Mesh Collider �̏Փ˂𖳎����܂���");
            }
            else
            {
                Debug.LogWarning("MeshCollider �܂��� Collider ��������܂���");
            }
        }
        else
        {
            Debug.LogError("�^�O 'Water' �̃I�u�W�F�N�g��������܂���");
        }
    }
}
