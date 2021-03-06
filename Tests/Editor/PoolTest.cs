﻿using NUnit.Framework;
using OptimizationTools.Pools;

namespace OptimizationTools.Tests {
  public class PoolTest {
    [SetUp]
    public void Init () {
      Pool<Vector2Container>.Init (1);
    }

    [Test]
    public void GetReferenceTest () {
      Vector2Container v = Pool<Vector2Container>.Get ();
      Assert.NotNull (v);
    }

    [Test]
    public void GetReferenceWhenExceedCapacityThrowsExceptionIfSafeModeIsNotActiveTest () {
#if !OT_SAFE_MODE
      Pool<Vector2Container>.Get ();
      Assert.Throws<System.ArgumentOutOfRangeException> (delegate {
        Pool<Vector2Container>.Get ();
      });
#else
      Assert.Ignore ("Test ignored on Safe Mode");
#endif
    }

    [Test]
    public void GetReferenceWhenExceedCapacityIncreasePoolSizeInsteadThrowingExceptionTest () {
#if OT_SAFE_MODE
      Pool<Vector2Container>.Get ();
      Assert.DoesNotThrow (delegate {
        Pool<Vector2Container>.Get ();
      });
#else
      Assert.Ignore ("Test ignored when Safe Mode is disabled");
#endif
    }

    [Test]
    public void ReleaseReferenceTest () {
      Vector2Container v = Pool<Vector2Container>.Get ();
      Pool<Vector2Container>.Release (v);
      Assert.DoesNotThrow (delegate {
        Pool<Vector2Container>.Get ();
      });
    }
  }
}