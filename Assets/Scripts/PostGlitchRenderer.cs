using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class PostGlitchRenderer : PostProcessEffectRenderer<PostGlitch>
{
    public PostGlitchRenderer()
    {

    }

    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/PostGlitch"));
        sheet.properties.SetTexture("_GlitchTex", settings.glitchTex);
        sheet.properties.SetFloat("_GlitchStrength", settings.glitch);
        sheet.properties.SetFloat("_NoiseStrength", settings.noise);
        sheet.properties.SetFloat("_Scale", settings.scale);
        sheet.properties.SetFloat("_TimeScale", settings.timeScale);
        sheet.properties.SetColor("_GlitchColor", settings.glitchColor);
        sheet.properties.SetFloat("_ColorSplitX", settings.colorSplitX);
        sheet.properties.SetFloat("_ColorSplitX", settings.colorSplitY);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
