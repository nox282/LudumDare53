using UnityEngine;

public class LoopTexture : MonoBehaviour
{
	public Vector2 scrollSpeed = new Vector2(0.5f, 0f); // The speed at which the texture scrolls
	private new Renderer renderer; // The sprite renderer
	private Material material; // The sprite's material
	private Vector2 offset = Vector2.zero; // The current offset of the texture coordinates

	private void Start()
	{
		// Get the renderer and material components
		renderer = GetComponent<Renderer>();
		material = renderer.material;
	}

	private void Update()
	{
		// Calculate the new offset of the texture coordinates
		offset += new Vector2(scrollSpeed.x * Time.deltaTime, scrollSpeed.y * Time.deltaTime);

		if(offset.x < -1)
		{
			offset.x += 1;
		}
		if (offset.x > 1)
		{
			offset.x -= 1;
		}

		if (offset.y < -1)
		{
			offset.y += 1;
		}
		if (offset.y > 1)
		{
			offset.y -= 1;
		}

		// Apply the new offset to the material
		material.SetTextureOffset("_MainTex", offset);
		
	}
}