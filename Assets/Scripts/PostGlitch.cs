using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PostGlitchRenderer), PostProcessEvent.AfterStack, "Custom/PostGlitch")]
public sealed class PostGlitch : PostProcessEffectSettings
{
    [Tooltip("Glitch Tex")]
    public TextureParameter glitchTex = new TextureParameter();

    [Range(0f, 1f), Tooltip("Noise Strength")]
    public FloatParameter noise = new FloatParameter { value = 0.0f };

    [Range(0f, 1f), Tooltip("Glitch Strength")]
    public FloatParameter glitch = new FloatParameter { value = 0.0f };

    [Range(0f, 2f), Tooltip("Scale")]
    public FloatParameter scale = new FloatParameter { value = 1f };

    [Range(0f, 5f), Tooltip("Time scale")]
    public FloatParameter timeScale = new FloatParameter { value = 1f };

    [Tooltip("Glitch Color")]
    public ColorParameter glitchColor = new ColorParameter { value = new Color(1.0F, 0.0F, 0.0F) };

    [Range(0f, 1f), Tooltip("Color Split X")]
    public FloatParameter colorSplitX = new FloatParameter { value = 0.5f };

    [Range(0f, 1f), Tooltip("Color Split Y")]
    public FloatParameter colorSplitY = new FloatParameter { value = 0.2f };
}

public sealed class PostGlitchRenderer : PostProcessEffectRenderer<PostGlitch>
{
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
