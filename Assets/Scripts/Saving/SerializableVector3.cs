using UnityEngine;

namespace RPG.Saving
{
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z, w;

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;

        }

        public SerializableVector3(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;

        }

        public Vector3 ToVector()
        {
            Vector3 vector = new Vector3(x, y, z);
            return vector;
        }

        public  Quaternion ToQuaternion()
        {
            Quaternion quaternion = new Quaternion(x, y, z, w);
            return quaternion;
        }
    }
}