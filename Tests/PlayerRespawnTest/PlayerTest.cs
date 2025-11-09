using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        var playerGo = new GameObject("Player");
        var playerRespawn = playerGo.AddComponent<PlayerRespawn>();
        playerRespawn.respawnPoint = new Vector2(-24.33f, -4.84f);

        var playerCollider = playerGo.AddComponent<BoxCollider2D>();
        var playerBody = playerGo.AddComponent<Rigidbody2D>();
        playerBody.gravityScale = 0; // so it doesn't fall infinitely

        // 2. Create the FallDetector
        var fallDetectorGo = new GameObject("FallDetector");
        fallDetectorGo.tag = "FallDetector";
        var detectorCollider = fallDetectorGo.AddComponent<BoxCollider2D>();
        detectorCollider.isTrigger = true;

        // 3. Place them in the same position so they overlap
        playerGo.transform.position = Vector2.zero;
        fallDetectorGo.transform.position = Vector2.zero;

        // 4. Wait a physics frame so Unity processes the trigger
        yield return new WaitForFixedUpdate();

        // 5. Assert that the player was respawned
        Assert.AreEqual(
            new Vector2(-24.33f, -4.84f),
            (Vector2)playerGo.transform.position,
            "Player should respawn after entering the FallDetector trigger."
        );

        // Cleanup
        Object.Destroy(playerGo);
        Object.Destroy(fallDetectorGo);

    }
}
