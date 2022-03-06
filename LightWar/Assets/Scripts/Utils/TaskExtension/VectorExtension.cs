namespace Assets.Scripts.Utils
{
    public static class VectorExtension
    {
        public static UnityEngine.Vector3 ToUnity(this System.Numerics.Vector3 vector)
        {
             return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
        }

        public static System.Numerics.Vector3 ToNumerics(this UnityEngine.Vector3 vector)
        {
            return new System.Numerics.Vector3(vector.x, vector.y, vector.z);        
        }
    }
}
