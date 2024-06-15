using HelloOpenGl.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace HelloOpenGl;

public class Game(int width, int height, string title)
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
    Shader shader;
    readonly float[] vertices = [
    -0.5f, -0.5f, 0.0f, //Bottom-left vertex
     0.5f, -0.5f, 0.0f, //Bottom-right vertex
     0.0f,  0.5f, 0.0f  //Top vertex
    ];
    int VBO;


    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        VBO = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        shader = new("./Shaders/shader.vert", "./Shaders/shader.frag");
        shader.Dispose();
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

        //Code goes here.

        SwapBuffers();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }
}
