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
				// Valid Changes
				yield return AddTransformUpdate;
				yield return CreateTransformChange;
				yield return CreateUpdateChange;
				yield return CreateDataChange;

				// Invalid Changes
				yield return NullChange;
				yield return NullData;
				// yield return EmptyChange; // Not Valid
				yield return EmptyTransformPacket;
				yield return EmptyUpdatePacket;
				yield return EmptyAddPacket;
			}
		}

		private static object[] NullChange => new object[]
		{
			null, new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Empty);
				Assert.That(pp.Updates, Is.Empty);
			}),
			false
		};

		private static object[] NullData => new object[]
		{
			new Change { Type = "Camera", Action = ChangeAction.Add, Payload = "Dummy Data" },
			new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Empty);
				Assert.That(pp.Updates, Is.Empty);
			}),
			false
		};

		private static object[] EmptyChange => new object[]
		{
			new Change { Type = "Lock", Action = ChangeAction.Locked }, new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Empty);
				Assert.That(pp.Updates, Is.Empty);
			}),
			true
		};

		private static object[] EmptyUpdatePacket => new object[]
		{
			new Change { Action = ChangeAction.Update, Payload = string.Empty }, new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Empty);
				Assert.That(pp.Updates, Is.Empty);
			}),
			false
		};

		private static object[] EmptyAddPacket => new object[]
		{
			new Change { Action = ChangeAction.Add, Payload = string.Empty }, new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Empty);
				Assert.That(pp.Updates, Is.Empty);
			}),
			false
		};

		private static object[] EmptyTransformPacket => new object[]
		{
			new Change { Action = ChangeAction.Transform, Payload = string.Empty }, new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Empty);
				Assert.That(pp.Updates, Is.Empty);
			}),
			false
		};

		private static object[] AddTransformUpdate =>
			new object[]
			{
				new Change
				{
					Action = ChangeAction.Update | ChangeAction.Add | ChangeAction.Transform,
					Payload = Serialize(GetFullPayload())
				},
				new Action<PayloadPacket>(pp =>
				{
					Assert.That(pp.Transform, Is.EqualTo(GetTransform()));
					Assert.That(pp.Data, Is.Not.Null.And.Not.Empty);
					Assert.That(pp.Updates, Is.Not.Null.And.Not.Empty);
				}),
				true
			};

		private static object[] CreateUpdateChange =>
			new object[]
			{
				new Change
				{
					Action = ChangeAction.Update, Payload = Serialize(new PayloadPacket { Updates = GetUpdate() })
				},
				new Action<PayloadPacket>(pp =>
				{
					Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
					Assert.That(pp.Data, Is.Empty);
					Assert.That(pp.Updates, Is.Not.Null.And.Not.Empty);
				}),
				true
			};

		private static object[] CreateTransformChange => new object[]
		{
			new Change
			{
				Action = ChangeAction.Transform,
				Payload = Serialize(new PayloadPacket { Transform = GetTransform() })
			},
			new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(GetTransform()));
				Assert.That(pp.Data, Is.Empty);
				Assert.That(pp.Updates, Is.Not.Null.And.Empty);
			}),
			true
		};

		private static object[] CreateDataChange => new object[]
		{
			new Change
			{
				Action = ChangeAction.Add, Payload = Serialize(new PayloadPacket { Data = GetPayloadData() })
			},
			new Action<PayloadPacket>(pp =>
			{
				Assert.That(pp.Transform, Is.EqualTo(CTransform.Unset));
				Assert.That(pp.Data, Is.Not.Null.And.Not.Empty);
				Assert.That(pp.Updates, Is.Empty);
			}),
			true
		};

		internal static string? GetPayloadData()
		{
			return nameof(PayloadTests);
		}

		internal static Dictionary<string, string> GetUpdate()
		{
			Dictionary<string, string> dict = new() { { "Key", "Value" } };
			return dict;
		}

		internal static PayloadPacket GetFullPayload()
		{
			return new PayloadPacket { Transform = GetTransform(), Data = GetPayloadData(), Updates = GetUpdate() };
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
		public void TryGetPayload_Success(IChange change, Action<PayloadPacket> validate, bool expected)
		{
			bool actual = PayloadUtils.TryGetPayloadFromChange(change, out PayloadPacket packet);
			Assert.That(expected, Is.EqualTo(actual));
			validate(packet);
		}
	}
}
