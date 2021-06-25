using UnityEngine;

namespace Assets.Scripts
{
    public class MenuMovement : MonoBehaviour
    {
        [SerializeField] private float menuMoveSpeed;

        private Rigidbody2D menuMoveX;
        // Start is called before the first frame update
        void Start()
        {
            menuMoveX = GetComponent<Rigidbody2D>();
        }

        // Upate is called once per frame
        void FixedUpdate()
        {
            menuMoveX.velocity = new Vector2(menuMoveSpeed * 10 * Time.deltaTime, menuMoveX.velocity.y);
        }
    }
}
