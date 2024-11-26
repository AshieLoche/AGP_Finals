using UnityEngine;

public class CharacterCollision : MonoBehaviour
{

    private bool _isPositioned = false;

    private void OnCollisionStay(Collision collision)
    {
        if (!_isPositioned)
        {
            if (collision.gameObject.CompareTag("Terrain"))
            {
                if (collision.gameObject.name == "Bench" || collision.gameObject.name == "Boat" || collision.gameObject.name == "Boat" || collision.gameObject.name.Contains("Fence") || collision.gameObject.name == "Center Piece" || collision.gameObject.name == "Flower and Stone" || collision.gameObject.name == "Stone Path" || collision.gameObject.name == "Tree")
                    transform.position = new Vector3(Random.Range(-1170f, 1170f), 140f, Random.Range(-1160f, 1160f));
            }
            else if (collision.gameObject.CompareTag("Peashooter") || collision.gameObject.CompareTag("Snow Pea") || collision.gameObject.CompareTag("Squash") || collision.gameObject.CompareTag("Sunflower"))
            {
                transform.position = new Vector3(Random.Range(-1170f, 1170f), 140f, Random.Range(-1160f, 1160f));
            }
            else
            {
                _isPositioned = true;
            }
        }
    }

}