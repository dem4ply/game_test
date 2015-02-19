using System.Collections;
using NUnit.Framework;

[TestFixture]
public class math_test {
	[Test]
	public void clamp_angle() {
		float result = helper.math.clamp_angle( 10f, 0f, 360f );
		Assert.That( result == 10f );
		result = helper.math.clamp_angle( 5f, 10f, 360f );
		Assert.That( result == 10f );
		result = helper.math.clamp_angle( 370f, 10f, 360f );
		Assert.That( result == 10f );
	}
}
