using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace ExampleMods {
	// this is how the vintagestory mod loader knows how to start our mod 
	class MyFirstCommand : ModSystem
	{
		// this tells the vs mod loader that this is a client side mod
		public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Client;

		// the vs mod loader uses this to start our mod for the client side
		public override void StartClientSide(ICoreClientAPI api) {
			// this creates a client side command called "hello" with the description "Says Hello!" 
			api.RegisterCommand("hello", "Says hello!", "hello", (int groupId, CmdArgs cmdArgs) => {
				// this says hello! :)
				api.ShowChatMessage("Hello!");
			});
		}
	}
}
