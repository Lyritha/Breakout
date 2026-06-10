using UnityEngine;

public class SpawnColliders : MonoBehaviour
{
   
   public Vector4 bounds;
   private void Awake()
   {
      bounds = ScreenWorldBounds.GetBounds(Camera.main);
      SpawnCollidersAtBounds();
   }
   
   private void SpawnCollidersAtBounds()
   {
      //Spawn something at bounds
      GameObject leftCollider = new GameObject("LeftCollider");
      BoxCollider2D leftBoxCollider = leftCollider.AddComponent<BoxCollider2D>();
      leftCollider.AddComponent<ShakeableWall>();
      leftCollider.layer = LayerMask.NameToLayer("Bounds");
      
      leftBoxCollider.size = new Vector3(0.1f, bounds.w - bounds.z, 1f);
      leftBoxCollider.transform.position = new Vector3(bounds.x, (bounds.w + bounds.z) * 0.5f, 0f);
      
      GameObject rightCollider = new GameObject("RightCollider");
      BoxCollider2D rightBoxCollider = rightCollider.AddComponent<BoxCollider2D>();
             rightCollider.AddComponent<ShakeableWall>();
         rightCollider.layer = LayerMask.NameToLayer("Bounds");
      
      rightBoxCollider.size = new Vector3(0.1f, bounds.w - bounds .z, 1f);
      rightBoxCollider.transform.position = new Vector3(bounds.y, (bounds.w + bounds.z) * 0.5f, 0f);
      
      GameObject topCollider = new GameObject("TopCollider");
      BoxCollider2D topBoxCollider = topCollider.AddComponent<BoxCollider2D>();
        topCollider.AddComponent<ShakeableWall>();
         topCollider.layer = LayerMask.NameToLayer("Bounds");
      
      topBoxCollider.size = new Vector3(bounds.y - bounds.x, 0.1f, 1f);
      topBoxCollider.transform.position = new Vector3((bounds.y + bounds.x) * 0.5f, bounds.w, 0f);    
      
      GameObject bottomCollider = new GameObject("BottomCollider");
      BoxCollider2D bottomBoxCollider = bottomCollider.AddComponent<BoxCollider2D>();
      KillBox onBallBottom = bottomCollider.AddComponent<KillBox>();
         bottomCollider.layer = LayerMask.NameToLayer("Bounds");
      
      bottomBoxCollider.size = new Vector3(bounds.y - bounds.x, 0.1f, 1f);
      bottomBoxCollider.transform.position = new Vector3((bounds.y + bounds.x) * 0.5f, bounds.z, 0f);    
   }
}
