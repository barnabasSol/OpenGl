using HelloOpenGl.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HelloOpenGl;

public class Window(int width, int height, string title)
    : GameWindow(
        GameWindowSettings.Default,
        new NativeWindowSettings()
        {
            ClientSize = (width, height),
            Title = title,
            Profile = ContextProfile.Core
        }
    )
{
    Shader shader = null!;
    Shader shader2 = null!;
    // float[] vertices = [
    //  0.2f,  0.2f, 0.0f,  // top right
    //  0.2f, -0.2f, 0.0f,  // bottom right
    // -0.2f, -0.2f, 0.0f,  // bottom left
    // -0.2f,  0.2f, 0.0f   // top left
    // ];
    
    float[] vertices = [
     -0.9f, -0.5f, 0.0f, //Bottom-left vertex
     -0.1f, -0.5f, 0.0f, //Bottom-right vertex
     -0.5f,  0.5f, 0.0f,  //Top vertex

    ];
    float[] vertices2 = [
      0.1f, -0.5f, 0.0f, //Bottom-left vertex
      0.9f, -0.5f, 0.0f, //Bottom-right vertex
      0.5f,  0.5f, 0.0f  //Top vertex
    ];

    uint[] indices = {  // note that we start from 0!
    0, 1, 3,   // first triangle
    1, 2, 3    // second triangle
    };

    int[] VAO = null!;
    int[] VBO = null!;
    // int EBO;

    protected override void OnLoad()
    { 
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        VAO = new int[2];
        VBO = new int[2];

        shader = new("./Shaders/shader.vert", "./Shaders/shader.frag");
        shader2 = new("./Shaders/glsf/shader.vert", "./Shaders/glsf/shader.frag");

        GL.GenVertexArrays(2, VAO);
        GL.GenBuffers(2, VBO);

        GL.BindVertexArray(VAO[0]);
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO[0]);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.BindVertexArray(VAO[1]);
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO[1]);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices2.Length * sizeof(float), vertices2, BufferUsageHint.StaticDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        // EBO = GL.GenBuffer();
        // GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
        // GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

    }


    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
        base.OnUpdateFrame(e);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Use();
        GL.BindVertexArray(VAO[0]);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        shader2.Use();
        GL.BindVertexArray(VAO[1]);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        // GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }
}
