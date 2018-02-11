using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DistributedAIBrainUpdater))]
public class AIBrain_DistributedUpdaterEditor : DistributedUpdaterEditor
{

	protected override int GetBucketsCount(Object target)
	{
		var updater = target as DistributedUpdater<AIBrain>;
		return updater == null ? -1 : updater.Editor_UpdatableBuckets.Count;
	}

	// --------------------------------------------------------------------------------

	protected override float GetBucketUpdateTime(Object target, int bucketIndex)
	{
		var updater = target as DistributedUpdater<AIBrain>;
		return updater == null ? -1.0f : updater.Editor_UpdatableBuckets[bucketIndex].m_updateTime;
	}

	// --------------------------------------------------------------------------------

	protected override int GetBucketContentsCount(Object target, int bucketIndex)
	{
		var updater = target as DistributedUpdater<AIBrain>;
		return updater == null ? -1 : updater.Editor_UpdatableBuckets[bucketIndex].m_updatables.Count;
	}

}