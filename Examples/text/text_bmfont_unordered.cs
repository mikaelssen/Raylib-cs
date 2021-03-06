using Raylib;
using static Raylib.Raylib;

public partial class text_bmfont_unordered
{
	/*******************************************************************************************
    *
    *   raylib [text] example - BMFont unordered chars loading and drawing
    *
    *   This example has been created using raylib 1.4 (www.raylib.com)
    *   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
    *
    *   Copyright (c) 2016 Ramon Santamaria (@raysan5)
    *
    ********************************************************************************************/


	public static int Main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		int screenWidth = 800;
		int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [text] example - bmfont unordered loading and drawing");

		// NOTE: Using chars outside the [32..127] limits!
		// NOTE: If a character is not found in the font, it just renders a space
		string msg = "ASCII extended characters:\n¡¢£¤¥¦§¨©ª«¬®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆ\nÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæ\nçèéêëìíîïðñòóôõö÷øùúûüýþÿ";

		// NOTE: Loaded font has an unordered list of characters (chars in the range 32..255)
		Font font = LoadFont("resources/pixantiqua.fnt");       // BMFont (AngelCode)

		SetTargetFPS(60);
		//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			// TODO: Update variables here...
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			DrawText("Font name:       PixAntiqua", 40, 50, 20, GRAY);
		    DrawText(string.Format("Font base size:           {0}", font.baseSize), 40, 80, 20, GRAY);
			DrawText(string.Format("Font chars number:     {0}", font.charsCount), 40, 110, 20, GRAY);

			DrawTextEx(font, msg, new Vector2(40, 180), font.baseSize, 0, MAROON);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		UnloadFont(font);     // AngelCode Font unloading

		CloseWindow();                // Close window and OpenGL context
									  //--------------------------------------------------------------------------------------

		return 0;
	}
}
