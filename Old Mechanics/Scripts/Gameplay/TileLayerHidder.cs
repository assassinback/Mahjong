// Author: Oleksii Stepanov

using System.Collections;
using UnityEngine;

namespace MahjongTemplateEditor
{
    /// <summary>
    /// Creates fading effect for the gameObject
    /// </summary>
    internal class TileLayerHidder : MonoBehaviour
    {
        /// <summary>
        /// Position of the final destination of the gameObject
        /// </summary>
        private Vector3 finishTargetPos;
        
        /// <summary>
        /// Color of the SpriteRenderer
        /// </summary>
        private Color colorOfTile;

        /// <summary>
        /// Transparent color of the SpriteRenderer
        /// </summary>
        private Color colorOfTileTransparent;

        /// <summary>
        /// SpriteRenderer of the GameObject
        /// </summary>
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Speed of the movement 
        /// </summary>
        private float speed = 10f;
        
        /// <summary>
        /// Step of the color movement
        /// </summary>
        private float stepOfColor = 0f;

        private void Start()
        {
            /// Sets up class
            SetUp();

            /// Destroys after 0.5f seconds
            Invoke("DestroyGameObject",0.5f);
        }

        /// <summary>
        /// Sets Up class variables
        /// </summary>
        private void SetUp() 
        {
            /// Sets up destination position 
            finishTargetPos = transform.position + new Vector3(0, 5, 0);

            /// Assigns SpriteRenderer component
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
            
            /// Assigns color of the SpriteRenderer
            colorOfTile = spriteRenderer.color;

            /// Sets color of the tile to final color of the tile 
            colorOfTileTransparent = colorOfTile;

            /// Sets alpha of the final color 
            colorOfTileTransparent.a = 0;
        }


        /// <summary>
        /// Destroys GameObject
        /// </summary>
        private void DestroyGameObject()
        {
            /// Destroys GameObject
            Destroy(gameObject);
        }

        private void Update()
        {
            /// Movement of the Object
            Move();

            /// Changes color of the object by time
            ChangeColor();
        }

        /// <summary>
        /// Method that controls movement of the 
        /// </summary>
        private void Move()
        {
            /// Creates a step of the movement
            float step = speed * Time.deltaTime;
            
            /// Sets new position of the gameObject
            transform.position = Vector3.MoveTowards(transform.position, finishTargetPos, step);
        }

        /// <summary>
        /// Method that changes color of the object by time
        /// </summary>
        private void ChangeColor()
        {
            /// Creates step of the color 
            stepOfColor += Time.deltaTime;
            
            /// Sets new color of the gameObject
            spriteRenderer.color = Color.Lerp(colorOfTile, colorOfTileTransparent, stepOfColor* 2);
        }
    }
}
