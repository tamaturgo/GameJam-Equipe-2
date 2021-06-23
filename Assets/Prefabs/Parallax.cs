using UnityEngine;

namespace Assets.Scripts
{
    public class Parallax : MonoBehaviour
    {
        private float length, startpos;
        public GameObject cam;
        public float parallaxEffecct;

        void Start()
        {
            startpos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            
        }

        void FixedUpdate()
        {

            float temp = (cam.transform.position.x * (1 - parallaxEffecct));
            float dist = (cam.transform.position.x * parallaxEffecct);
            transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - length) startpos -= length;
        }
        
        

    }
}
