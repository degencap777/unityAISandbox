

public class BSPNode : BTreeNode<BSPPartition>
{

	public BSPNode(BSPPartition data, BTreeNode<BSPPartition> parent) 
		: base(data, parent)
	{
	}

	// --------------------------------------------------------------------------------

	// #SteveD	>>> subdivide
	//			>>> add child nodes
	//			>>> insert this node's agents into child nodes

	// --------------------------------------------------------------------------------

	// #SteveD	>>> combine
	//			>>> insert child node agents into this node
	//			>>> remove child nodes

}
