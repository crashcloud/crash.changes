using Crash.Changes.Utils;

// ReSharper disable HeapView.BoxingAllocation

namespace Crash.Changes.Tests.Utils
{
	public class CombinePayloadsTests
	{
		public static object[] CombinedTransforms =
		{
			new PayloadPacket { Transform = PayloadTests.GetTransform() },
			new PayloadPacket { Transform = PayloadTests.GetTransform() }, new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.Not.EqualTo(CTransform.Unset));
				Assert.That(pp.Updates, Is.Empty);
				Assert.That(pp.Data, Is.Empty);
			})
		};

		public static object[] OverwrittenData =
		{
			new PayloadPacket { Data = string.Empty }, new PayloadPacket { Data = nameof(OverwrittenData) },
			new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Updates, Is.Empty);
				Assert.That(pp.Data, Is.EqualTo(nameof(OverwrittenData)));
			})
		};

		public static object[] CombinedUpdates =
		{
			new PayloadPacket { Updates = new Dictionary<string, string> { { "Key1", "Value1 " } } },
			new PayloadPacket { Updates = new Dictionary<string, string> { { "Key2", "Value2" } } },
			new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Updates, Is.Not.Empty);
				Assert.That(pp.Updates.Count(), Is.EqualTo(2));
				Assert.That(pp.Data, Is.Empty);
			})
		};

		public static IEnumerable ValidPayloadData
		{
			get
			{
				yield return CombinedTransforms;
				yield return OverwrittenData;
				yield return CombinedUpdates;
			}
		}

		private static IEnumerable InvalidPayloadData
		{
			get
			{
				yield return new object[] { null, null };
			}
		}

		[Theory]
		[TestCaseSource(nameof(ValidPayloadData))]
		public void CombinePayloads_Success(PayloadPacket left, PayloadPacket right, Action<PayloadPacket> validate)
		{
			PayloadPacket result = PayloadUtils.Combine(left, right);
			validate(result);
		}

		[Theory]
		[TestCaseSource(nameof(InvalidPayloadData))]
		public void CombinePayloads_Failure(PayloadPacket left, PayloadPacket right)
		{
			PayloadPacket result = PayloadUtils.Combine(left, right);

			Assert.That(result.Transform, Is.EqualTo(CTransform.Unset));
			Assert.That(result.Data, Is.Empty);
			Assert.That(result.Updates, Is.Empty);
		}
	}
}
