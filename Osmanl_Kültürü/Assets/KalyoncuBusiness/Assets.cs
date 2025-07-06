using UnityEngine;

namespace KalyoncuBusiness
{
    public class Assets : MonoBehaviour
    {

        // Internal instance reference
        private static Assets _i;

        // Instance reference
        public static Assets i
        {
            get
            {
                if (_i == null) _i = Instantiate(Resources.Load<Assets>("KalyoncuBusinessAssets"));
                return _i;
            }
        }


        // All references

        [Header("Sprites")]

        public Sprite s_White;
        public Sprite s_Circle;
        public Sprite s_SelectFrame;

        [Header("Particles")]
        public GameObject p_Blood;
        public GameObject p_Heal;

        [Header("Materials")]
        public Material m_White;

    }

}
