using UnityEngine;
using System.Collections;

public class Linear : MonoBehaviour
{
	// linear proportion - unclamped //
	public static float unclamped(float output_1, float output_2, float input_1, float input_2, float input)
	{
		float slope = (output_2 - output_1) / (input_2 - input_1);      // both the numerator and the denominator should precisely be absolute difference, but here they are effectively since parities cancel out //
		return slope * input + output_1 - slope * input_1;
	}
	// linear proportion - clamped/interpolation //
	public static float clamped(float output_1, float output_2, float input_1, float input_2, float input)
	{
		float linear_value = unclamped(output_1, output_2, input_1, input_2, input);
		if (linear_value < output_1)
			linear_value = output_1;
		else if (linear_value > output_2)
			linear_value = output_2;
		return linear_value;
	}
}
