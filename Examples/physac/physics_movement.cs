using Raylib;
using static Raylib.Raylib;

public partial class physics_movement
{
    /*******************************************************************************************
    *
    *   Physac - Physics movement
    *
    *   NOTE 1: Physac requires multi-threading, when InitPhysics() a second thread is created to manage physics calculations.
    *   NOTE 2: Physac requires static C library linkage to avoid dependency on MinGW DLL (-static -lpthread)
    *
    *   Use the following line to compile:
    *
    *   gcc -o $(NAME_PART).exe $(FILE_NAME) -s $(RAYLIB_DIR)\raylib\raylib.rc.o -static -lraylib -lpthread
    *   -lglfw3 -lopengl32 -lgdi32 -lopenal32 -lwinmm -std=c99 -Wl,--subsystem,windows -Wl,-allow-multiple-definition
    *
    *   Copyright (c) 2016-2018 Victor Fisac
    *
    ********************************************************************************************/



    public const float VELOCITY = 0.5f;

    public static int Main()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        int screenWidth = 800;
        int screenHeight = 450;

        SetConfigFlags(FLAG_MSAA_4X_HINT);
        InitWindow(screenWidth, screenHeight, "Physac [raylib] - Physics movement");

        // Physac logo drawing position
        int logoX = screenWidth - MeasureText("Physac", 30) - 10;
        int logoY = 15;

        // Initialize physics and default physics bodies
        InitPhysics();

        // Create floor and walls rectangle physics body
        PhysicsBodyData floor = CreatePhysicsBodyRectangle(new Vector2( screenWidth/2, screenHeight ), screenWidth, 100, 10);
        PhysicsBodyData platformLeft = CreatePhysicsBodyRectangle(new Vector2( screenWidth*0.25f, screenHeight*0.6f ), screenWidth*0.25f, 10, 10);
        PhysicsBodyData platformRight = CreatePhysicsBodyRectangle(new Vector2( screenWidth*0.75f, screenHeight*0.6f ), screenWidth*0.25f, 10, 10);
        PhysicsBodyData wallLeft = CreatePhysicsBodyRectangle(new Vector2( -5, screenHeight/2 ), 10, screenHeight, 10);
        PhysicsBodyData wallRight = CreatePhysicsBodyRectangle(new Vector2( screenWidth + 5, screenHeight/2 ), 10, screenHeight, 10);

        // Disable dynamics to floor and walls physics bodies
        floor.enabled = false;
        platformLeft.enabled = false;
        platformRight.enabled = false;
        wallLeft.enabled = false;
        wallRight.enabled = false;

        // Create movement physics body
        PhysicsBodyData body = CreatePhysicsBodyRectangle(new Vector2( screenWidth/2, screenHeight/2 ), 50, 50, 1);
        body.freezeOrient = true;  // Constrain body rotation to avoid little collision torque amounts

        SetTargetFPS(60);
        //--------------------------------------------------------------------------------------

        // Main game loop
        while (!WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            RunPhysicsStep();

            if (IsKeyPressed('R'))    // Reset physics input
            {
                // Reset movement physics body position, velocity and rotation
                body.position = new Vector2( screenWidth/2, screenHeight/2 );
                body.velocity = new Vector2( 0, 0 );
                SetPhysicsBodyRotation(body, 0);
            }

            // Horizontal movement input
            if (IsKeyDown(KEY_RIGHT)) body.velocity.x = VELOCITY;
            else if (IsKeyDown(KEY_LEFT)) body.velocity.x = -VELOCITY;

            // Vertical movement input checking if player physics body is grounded
            if (IsKeyDown(KEY_UP) && body.isGrounded) body.velocity.y = -VELOCITY*4;
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            BeginDrawing();

                ClearBackground(BLACK);

                DrawFPS(screenWidth - 90, screenHeight - 30);

                // Draw created physics bodies
                int bodiesCount = GetPhysicsBodiesCount();
                for (int i = 0; i < bodiesCount; i++)
                {
                    var ibody = GetPhysicsBody(i);

                    int vertexCount = GetPhysicsShapeVerticesCount(i);
                    for (int j = 0; j < vertexCount; j++)
                    {
                        // Get physics bodies shape vertices to draw lines
                        // Note: GetPhysicsShapeVertex() already calculates rotation transformations
                        Vector2 vertexA = GetPhysicsShapeVertex(ibody, j);

                        int jj = (((j + 1) < vertexCount) ? (j + 1) : 0);   // Get next vertex or first to close the shape
                        Vector2 vertexB = GetPhysicsShapeVertex(ibody, jj);

                        DrawLineV(vertexA, vertexB, GREEN);     // Draw a line between two vertex positions
                    }
                }

                DrawText("Use 'ARROWS' to move player", 10, 10, 10, WHITE);
                DrawText("Press 'R' to reset example", 10, 30, 10, WHITE);

                DrawText("Physac", logoX, logoY, 30, WHITE);
                DrawText("Powered by", logoX + 50, logoY - 7, 10, WHITE);

            EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        ClosePhysics();       // Unitialize physics

        CloseWindow();        // Close window and OpenGL context
        //--------------------------------------------------------------------------------------

        return 0;
    }
}
