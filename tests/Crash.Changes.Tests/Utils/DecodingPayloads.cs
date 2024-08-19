using System;

using Crash.Changes.Utils;

namespace Crash.Changes.Tests.Utils;

public class DecodingPayloads
{
	private const string PerfectData = "{\"version\":10000,\"archive3dm\":70,\"opennurbs\":-1876390726,\"data\":\"+n8CAOkAAAAAAAAA+/8CABQAAAAAAAAA5tTXTkfp0xG/5QAQgwEi8E6cu9v8/wIAsQAAAAAAAAAQBQAAAAAAAAAAAELAAAAAAAAAQkAAAAAAAAAAAAAAAAAAABRAAAAAAAAAQkAAAAAAAAAAAAAAAAAAABRAAAAAAAAAREAAAAAAAAAAAAAAAAAAAELAAAAAAAAAREAAAAAAAAAAAAAAAAAAAELAAAAAAAAAQkAAAAAAAAAAAAUAAAAAAAAAAAAAAAAAAAAAgERAAAAAAACARkAAAAAAAIBVQAAAAAAAgFZAAwAAAKtlCBv/fwKAAAAAAAAAAAA=\"}";

	private const string FromClient = "{\"Data\":\"{\\u0022version\\u0022:10000,\\u0022archive3dm\\u0022:70,\\u0022opennurbs\\u0022:-1876390726,\\u0022data\\u0022:\\u0022\\u002Bn8CAOkAAAAAAAAA\\u002B/8CABQAAAAAAAAA5tTXTkfp0xG/5QAQgwEi8E6cu9v8/wIAsQAAAAAAAAAQBQAAAAAAAAAAAELAAAAAAAAAQkAAAAAAAAAAAAAAAAAAABRAAAAAAAAAQkAAAAAAAAAAAAAAAAAAABRAAAAAAAAAREAAAAAAAAAAAAAAAAAAAELAAAAAAAAAREAAAAAAAAAAAAAAAAAAAELAAAAAAAAAQkAAAAAAAAAAAAUAAAAAAAAAAAAAAAAAAAAAgERAAAAAAACARkAAAAAAAIBVQAAAAAAAgFZAAwAAAKtlCBv/fwKAAAAAAAAAAAA=\\u0022}\",\"Transform\":[[\"1\",\"0\",\"0\",\"0\"],[\"0\",\"1\",\"0\",\"0\"],[\"0\",\"0\",\"1\",\"0\"],[\"0\",\"0\",\"0\",\"1\"]],\"Updates\":{}}";

	[Test]
	public void RemoveUnicodeEscapes()
	{
		var newChange = new Crash.Changes.Change()
		{
			Payload = FromClient
		};

		Assert.True(PayloadUtils.TryGetPayloadFromChange(newChange, out var packet));
		Assert.That(packet.Data, Is.EqualTo(PerfectData));
	}

}
