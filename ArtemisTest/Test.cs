using System;
using NUnit.Framework;
using Artemis;
using ArtemisTest.Components;
using ArtemisTest.System;

namespace ArtemisTest
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestCase ()
		{
			World world = new World();
			SystemManager systemManager = world.GetSystemManager();
			EntitySystem hs = systemManager.SetSystem(new HealthBarRenderSystem());
			systemManager.InitializeAll();
			Entity e = world.CreateEntity();
		    e.AddComponent(new Health(100));
		    e.Refresh();
			int entityId = e.GetId();
			for(int i = 0;i < 10;i++) {
                world.LoopStart();
				float oldHealth = e.GetComponent<Health>().GetHealth();
				world.SetDelta(i);
	            hs.Process();
				float newHealth = e.GetComponent<Health>().GetHealth();
				Assert.Greater(oldHealth,newHealth);
			}
            world.LoopStart();
			float actualHealth = e.GetComponent<Health>().GetHealth();
			Assert.IsTrue(actualHealth == 0);
			world.DeleteEntity(e);
			Assert.IsTrue(world.GetEntity(entityId) == null);
		}
	}
}

