using NUnit.Framework;
using UnityEngine;
public class CountdownTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerTimeTestSimplePasses()
    {
        // Create a GameObject and add Countdown script
        var go = new GameObject();
        var countdown = go.AddComponent<Countdown>();
        countdown.countdownTime = 60f;

        // Simulate 1 second passing
        countdown.Tick(1f);
        Assert.AreEqual(59f, countdown.countdownTime, 0.01f);

        // Simulate 59 more seconds
        countdown.Tick(59f);
        Assert.AreEqual(0f, countdown.countdownTime, 0.01f);

        // Cleanup
        Object.DestroyImmediate(go);    }

   
}
