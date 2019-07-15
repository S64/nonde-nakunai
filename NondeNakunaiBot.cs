using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using S64.Bot.Builder.Adapters.Slack;

namespace NondeNakunai
{

    class NondeNakunaiBot : ActivityHandler
    {

        private readonly string[] suffixes = new string[] {
            "なくない?", "無くない?", "なくない？", "無くない？"
        };
        private readonly string[] messages = new string[] {
            "ｳｫｳ ｳｫｳ", "うぉううぉう", "wow wow"
        };

        protected override async Task OnMessageActivityAsync(
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken
        )
        {
            if (!SlackAdapter.CHANNEL_ID.Equals(turnContext.Activity.ChannelId))
            {
                throw new NotImplementedException();
            }

            var data = turnContext.Activity.ChannelData as SlackChannelData;

            if (!turnContext.Activity.Type.Equals(ActivityTypes.Message))
            {
                return;
            }

            foreach (var pattern in suffixes)
            {
                if (turnContext.Activity.Text.EndsWith(pattern))
                {
                    await turnContext.SendActivityAsync(
                        MessageFactory.Text(
                            messages.OrderBy(_ => Guid.NewGuid()).First()
                        ),
                        cancellationToken
                    );
                    break;
                }
            }
        }

    }

}
