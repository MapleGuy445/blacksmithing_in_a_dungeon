using System;
using Sandbox;
using Sandbox.Utility;

public sealed class LightFlicker : Component
{
	[Property] public float BaseAttenuation { get; set; } = 1.0f;
	[Property] public float FlickerAmount { get; set; } = 0.4f;
	[Property] public float FlickerSpeed { get; set; } = 8.0f;
	[Property] public float SmoothTime { get; set; } = 0.05f;

	private PointLight _light;
	private float _seed;
	private float _currentAttenuation;
	private float _velocity;

	protected override void OnStart()
	{
		_light = Components.Get<PointLight>();
		_seed = Game.Random.Float( 0f, 1000f );
		_currentAttenuation = BaseAttenuation;
	}

	protected override void OnUpdate()
	{
		if ( _light is null )
			return;

		// Sample noise over time for an organic flicker rather than pure random jitter
		float t = Time.Now * FlickerSpeed;
		float noise = Noise.Perlin( t + _seed );

		// A second, faster octave adds a bit of "crackle" on top of the slow wave
		float crackle = Noise.Perlin( ( t + _seed ) * 3.5f ) * 0.3f;

		float target = BaseAttenuation + ( noise + crackle ) * FlickerAmount;
		target = MathF.Max( target, 0.05f );

		// Smooth towards the target so it doesn't look like it's teleporting between values
		_currentAttenuation = MathX.SmoothDamp( _currentAttenuation, target, ref _velocity, SmoothTime, Time.Delta );

		_light.Attenuation = _currentAttenuation;
	}
}