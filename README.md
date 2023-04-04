# The_Dark
PITCH:
https://youtu.be/NzBa7QYCGUw

TRELLO:
https://trello.com/b/Mngml54B/graphics  (Must me logged into a trello account to view)

https://trello.com/invite/b/Mngml54B/ATTI8557c34be66d413bc95b99b1839e8d2903D7D71B/graphics  (Backup link if the first does not work)

CAPTIONS:
Hello, my name is Michael Giorgi. I am the lead programmer of Cavelight Studios. 

We are developing a Fps/Mystery/Horror game where the players objective is to explore and uncover the secrets of a hidden facility underneath an old hotel

We will be using CG to create a toon shader as well as an inked line shader to create a visual style somewhere between an Archer comic and the borderlands series. We will be implementing Specular, and Toon ramp, as well any other Illumination fields we find thats fits or enchances the style. We will also be implementing fog, particles, ambient occlusion, shadows and bloom. Another feature I would like is a lensflare effect that is also affected by the Toon shader and/or the ink lines shader.

I will be attempting to implement the majourity of this my self as I am the only memeber of my group in this class.

I belive that throughout this course I will be able to develop all of these CG features, and implement them in a non-performance intensive mannor, to keep the game smooth and playable.


# Group Assignment
Feb 26, 2023

Contributing Members: Michael Giorgi

Integrating from the individual assignmnet; I have integrated illumination and toon shader from the idividual assignment
Modifications: The toon shader has been re-writen as a vertex shader, and shadows as well as a second shadow caster pass have been added. The reverse hull method to render outlines has been removed and replaced with a post processing effect.

Description: Our game is a first person fps game which has puzzle elements and mystery solving as well as a somewhat comedic view of itself with a variety of weapons to use as well as areas to explore

Contributions (Michael Giorgi): All scripts except for those under the 'DetectiveInteraction' folder have been written 100% by me

Visual effects: I have implemented two particles sytems, one for the gun sparks, and one for the muzzle flash/smoke

Postprocessing Effects: I have implemented Bloom as well as a screen space outline effect.
Bloom: There are two main bloom effects one on the overhead lights to give the illusion they are being used to illuminate the scene, and one on the muzzle of the guns to add a glowing effect durring consecutive firing.

Known Issues: Since adding shadows unity has broken and every build even those with shadows removed opens with a black screen on the main game scene

Video Report: https://youtu.be/T6SlbqBx2sg

Presentation: https://docs.google.com/presentation/d/1fISQ-rJypNm8CEsS6Y9XYnj23unxz7zCNwczh_2-a7M/edit?usp=sharing

# Final Project
April 4, 2023

Contributing Members: Michael Giorgi
Build: The_Dark_GraphicsFinal/GameBuilds/FinalProjBuild/

Welcome to my video presentation on the technical aspects of my game's visual design. In this video, we will be discussing the shaders, texturing, color grading, and particle effects used to create the game's unique visual 
atmosphere.

My game utilizes two main shaders: the Toon Shader which also implements Outlines and Shadows, and the Glass Shader.

Toon shading, also known as cel shading, is a technique that gives 3D objects a stylized, cartoon-like appearance. The main components of a toon shader include customizable properties, the SubShader, and the CGPROGRAM block.

We'll create a custom lighting model called LightingToonRamp, which calculates the diffuse lighting by taking the dot product of the surface normal and the light direction. The ramp texture is then used to modulate the final shading 
output, resulting in the characteristic toon shading look.
<img width="476" alt="toonNoShadow" src="https://user-images.githubusercontent.com/94036650/229946464-d6896928-4575-4852-ab13-888d8b87515a.png">
<img width="247" alt="toonNoOutlines" src="https://user-images.githubusercontent.com/94036650/229946585-dd8fc2e3-60dc-48a9-a01e-09beb9ffe803.png">

Adding outlines to the 3D objects can make them pop and improve their visibility in the scene. The outlining algorithm can be implemented within a Pass block, and we'll use the Cull Front directive to ensure only 
back-facing triangles are rendered during this pass.
The vert function will enlarge the object's geometry based on the outline thickness, while the frag function simply returns the outline color as the final output.
<img width="525" alt="outlinePass" src="https://user-images.githubusercontent.com/94036650/229946499-3adcd8e5-6d55-4c09-9499-a702331bc1a0.png">
<img width="473" alt="Screenshot 2023-04-04 180229" src="https://user-images.githubusercontent.com/94036650/229946513-eb23c70e-8241-4494-ac38-516af112212e.png">
<img width="384" alt="ToonShader_Outlines" src="https://user-images.githubusercontent.com/94036650/229946610-62458557-3858-4bd2-8b1f-0b9f0f362ae3.png">

Shadows add depth and realism to the scene. I will calculate the light intensity, smooth the light intensity, and calculate the shadow factor based on the light attenuation value. The final color is computed by 
blending the shadow color and lit color, creating the desired shadow effect on the toon-shaded surface.
<img width="512" alt="toonShadow" src="https://user-images.githubusercontent.com/94036650/229946537-b4303a5b-a128-4f88-8cf5-3a44c6029062.png">
<img width="374" alt="ToonShader_Outlines_Shadows" src="https://user-images.githubusercontent.com/94036650/229946628-ad1b335c-a2e6-4249-9d0b-cd99c869ff75.png">

Realistic glass materials can be achieved by implementing refraction and distortion in the shader. I wil use a GrabPass to capture the current screen content and sample the scene behind the glass.

The vertex shader function calculates the transformed UV coordinates, and the fragment shader samples the bump map and extracts the distortion offset. The final output color is obtained by multiplying the grab pass color with 
the main texture color.
<img width="414" alt="glass1" src="https://user-images.githubusercontent.com/94036650/229946686-37654282-32d2-4c6e-b881-63b999e234bb.png">
<img width="585" alt="glass2" src="https://user-images.githubusercontent.com/94036650/229946693-2e1c5df9-082e-4705-8307-581c6ca9cd17.png">
<img width="389" alt="glassssssss" src="https://user-images.githubusercontent.com/94036650/229946708-6a32c7b6-e76d-465e-8bec-9e640192c5c6.png">
<img width="679" alt="glassss" src="https://user-images.githubusercontent.com/94036650/229946713-5db2c0e5-467f-426c-98f2-c1b4e2a3eda2.png">

My texturing process involves a combination of Blender and image editing software. The models are unwrapped, and their UV maps are imported into an image editing software to create the textures. The textures are then applied to the corresponding models using the appropriate shaders in Unity. The chosen color palette contributes to the surreal, dark cartoon atmosphere of the game.
![Options-A23D-How-to-use-Smart-UV-unwrap-in-Blender](https://user-images.githubusercontent.com/94036650/229946744-d4333caf-4642-4721-93e4-f99854adcf8e.jpg)
![Untitled drawing (4)](https://user-images.githubusercontent.com/94036650/229946803-1817cc2d-0ee8-425f-a972-4ee96c6c1e0b.png)
![Untitled drawing (5)](https://user-images.githubusercontent.com/94036650/229946809-e064cf57-216c-4916-bf9b-7f34c2c7c302.png)

I have applied two types of color correction to set the mood in different areas of the game. The cool color correction creates a distant, uneasy atmosphere, while the warm color correction adds warmth and a false sense of safety for the player.
![flowChart 3](https://user-images.githubusercontent.com/94036650/229946889-7cb51a16-cf0f-4213-945f-f5e85683884a.png)
![NeutralLUT](https://user-images.githubusercontent.com/94036650/229946845-b327147b-cf28-4350-82ef-02a1246fc5f6.png)
![CoolLUT](https://user-images.githubusercontent.com/94036650/229946852-3c5e14a7-a8fc-46be-a6db-ba2b01dfde30.png)
![WarmLUT](https://user-images.githubusercontent.com/94036650/229946866-81a5e2f7-3f16-418e-9024-36ff305ac81b.png)

Particle effects play a crucial role in enhancing the game's visuals. For example, when the player fires a gun, a bullet spark particle system is emitted, and a bright white smoke effect is created, which then turns into 
a glowing orange before fading to dark grey. Additionally, we have added a subtle lens flare effect to each light source in the game.
<img width="678" alt="Screenshot 2023-04-04 190650" src="https://user-images.githubusercontent.com/94036650/229946951-839afc5f-c438-46fa-973e-7997ef4a4cbf.png">
<img width="668" alt="smoke" src="https://user-images.githubusercontent.com/94036650/229946959-ae445b4b-d07a-40c9-97b5-156757fd0216.png">
<img width="524" alt="LensFlare_Progress" src="https://user-images.githubusercontent.com/94036650/229946968-7451475d-8c0a-4b64-8506-0587a4799f09.png">

Bloom is a post-processing effect that enhances the glow of bright areas in the rendered image. To use bloom, enable it in your camera's post-processing settings and adjust the intensity threshold, intensity, and radius.
<img width="365" alt="Bloom_Progress2" src="https://user-images.githubusercontent.com/94036650/229946975-9a1f6114-44fa-4676-a99d-624eb8b18856.png">
<img width="278" alt="Screenshot 2023-04-04 115503" src="https://user-images.githubusercontent.com/94036650/229947032-df849dfc-c718-41e8-b850-1a862610c5a4.png">
<img width="353" alt="bloom1" src="https://user-images.githubusercontent.com/94036650/229947006-ea0ac5eb-d083-4576-a018-673227860f4e.png">

For materials with emissive properties, enable the Emission property, set the Emission Color, and optionally assign an Emission Texture.

Fog can be used to add atmosphere and depth to your scene. In Unity, enable fog in the Lighting settings and choose from three fog modes: Linear, Exponential, and Exponential Squared. Set the fog color and adjust the fog density, start, and end distances as needed.
<img width="510" alt="Fog_Progress" src="https://user-images.githubusercontent.com/94036650/229947073-6bbb0eaa-ecff-471b-bb5d-90b18d51284d.png">
<img width="194" alt="Screenshot 2023-04-04 190127" src="https://user-images.githubusercontent.com/94036650/229947095-428813d6-b1cc-4127-8fe6-421ad34fedf4.png">

My game's visual design is a combination of custom shaders, textures, color grading, and particle effects that come together to create a unique, stylized atmosphere. Thank you for watching this presentation on my game's visual design.

Video Report: https://youtu.be/fv8ZM33vryQ

Presentation: https://docs.google.com/presentation/d/1CasEwB_YqytlqF1mf-YV3l4Jxl3ujfmt4UBelfGe7rM/edit?usp=sharing
