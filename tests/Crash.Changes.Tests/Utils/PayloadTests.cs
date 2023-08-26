using System.Text.Json;

using Crash.Changes.Utils;

// ReSharper disable HeapView.BoxingAllocation

namespace Crash.Changes.Tests.Utils
{
	public class PayloadTests
	{
		public static IEnumerable PayloadData
		{
			get
			{
				yield return CreateTransformChange;
				yield return CreateUpdateChange;
				yield return CreateDataChange;
			}
		}

		public static object[] CreateUpdateChange =>
			new object[]
			{
				new Change { Action = ChangeAction.Update, Payload = Serialize(GetUpdate()) },
				new Action<PayloadPacket>(pp =>
				{
					Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
					Assert.That(pp.Data, Is.Empty);
					Assert.That(pp.Updates, Is.Not.Null.And.Empty);
				})
			};

		private static object[] CreateTransformChange => new object[]
		{
			new Change { Action = ChangeAction.Transform, Payload = Serialize(GetTransform()) },
			new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.Not.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Empty);
				Assert.That(pp.Updates, Is.Not.Null.And.Empty);
			})
		};

		private static object[] CreateDataChange => new object[]
		{
			new Change { Action = ChangeAction.Add, Payload = GetPayloadData() }, new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Not.Null.And.Not.Empty);
				Assert.That(pp.Updates, Is.Empty);
			})
		};

		private static IEnumerable InvalidPayloadData
		{
			get
			{
				yield return null;
				yield return new Change();
			}
		}

		internal static string? GetPayloadData()
		{
			return nameof(PayloadTests);
		}

		internal static Dictionary<string, string> GetUpdate()
		{
			Dictionary<string, string> dict = new() { { "Key", "Value" } };
			return dict;
		}

		internal static CTransform GetTransform()
		{
			CTransform transform = new(100, 200, 300, 400);
			return transform;
		}

		private static string? Serialize(object value)
		{
			return JsonSerializer.Serialize(value);
		}

		[Theory]
		[TestCaseSource(nameof(PayloadData))]
		public void TryGetPayload_Success(IChange change, Action<PayloadPacket> validate)
		{
			Assert.That(PayloadUtils.TryGetPayloadFromChange(change, out PayloadPacket packet), Is.True);
			validate(packet);
		}

		[Theory]
		[TestCaseSource(nameof(InvalidPayloadData))]
		public void TryGetPayload_Failure(IChange change)
		{
			Assert.That(PayloadUtils.TryGetPayloadFromChange(change, out PayloadPacket packet), Is.False);
			Assert.That(packet.Data, Is.EqualTo(CTransform.Unset));
			Assert.That(packet.Transform, Is.EqualTo(CTransform.Unset));
			Assert.That(packet.Updates, Is.Not.Null.And.Not.Empty);
		}
	}
}
