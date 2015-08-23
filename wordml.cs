using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;
using System.Globalization;

namespace WordML
{
	public class WordMLNodeList : IEnumerable, IEnumerator
	{
		System.Xml.XmlNodeList m_nodes;
		XmlNamespaceManager m_nsmgr;
		IEnumerator m_ienumDelegate;

		/* G E T  E N U M E R A T O R */
		/*----------------------------------------------------------------------------
			%%Function: GetEnumerator
			%%Qualified: md.WordMLNodeList.GetEnumerator
			%%Contact: rlittle

		----------------------------------------------------------------------------*/
		public IEnumerator GetEnumerator()
		{
			m_ienumDelegate = m_nodes.GetEnumerator();
			return (IEnumerator)this;
		}

		/* M O V E  N E X T */
		/*----------------------------------------------------------------------------
			%%Function: MoveNext
			%%Qualified: md.WordMLNodeList.MoveNext
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public bool MoveNext()
		{
			return m_ienumDelegate.MoveNext();
		}

		/* C U R R E N T  _ G E T */
		/*----------------------------------------------------------------------------
			%%Function: Current_get
			%%Qualified: md.WordMLNodeList.Current_get
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public object Current
		{
			get
			{
				XmlNode node = (XmlNode)m_ienumDelegate.Current;
				return new WordMLNode(node, m_nsmgr);
			}
		}

		/* R E S E T */
		/*----------------------------------------------------------------------------
			%%Function: Reset
			%%Qualified: md.WordMLNodeList.Reset
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public void Reset()
		{
			m_ienumDelegate.Reset();
		}

		/* W O R D  M  L  N O D E  L I S T */
		/*----------------------------------------------------------------------------
			%%Function: WordMLNodeList
			%%Qualified: md.WordMLNodeList.WordMLNodeList
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNodeList(XmlNodeList nodes, XmlNamespaceManager nsmgr)
		{
			m_nodes = nodes;
			m_nsmgr = nsmgr;
		}
		
		/* I T E M */
		/*----------------------------------------------------------------------------
			%%Function: Item
			%%Qualified: md.WordMLNodeList.Item
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNode Item(int index)
		{
			return new WordMLNode(m_nodes.Item(index), m_nsmgr);
		}

		/* C O U N T  _ G E T */
		/*----------------------------------------------------------------------------
			%%Function: Count_get
			%%Qualified: md.WordMLNodeList.Count_get
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public int Count
		{
			get 
                {
				return m_nodes.Count;
				}
		}


	}

	public class WordMLNode
	{
		System.Xml.XmlNode m_node;
		XmlNamespaceManager m_nsmgr;

		public XmlNode InternalNode
		{
			get
			{
				return m_node;
			}
		}

		/* W O R D  M  L  N O D E */
		/*----------------------------------------------------------------------------
			%%Function: WordMLNode
			%%Qualified: md.WordMLNode.WordMLNode
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNode(XmlNode node, XmlNamespaceManager nsmgr)
		{
			m_node = node;
			m_nsmgr = nsmgr;
		}

		
		public WordMLDocument OwnerDocument
		{
			get
			{
				return new WordMLDocument(m_node.OwnerDocument);
			}
		}

		/* A T T R I B U T E S  _ G E T */
		/*----------------------------------------------------------------------------
			%%Function: Attributes_get
			%%Qualified: md.WordMLNode.Attributes_get
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public XmlAttributeCollection Attributes
		{
			get
			{
				return m_node.Attributes;
			}
		}

		/* N A M E  _ G E T */
		/*----------------------------------------------------------------------------
			%%Function: Name_get
			%%Qualified: md.WordMLNode.Name_get
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public string Name
		{
			get
			{
				return m_node.Name;
			}
		}

		/* N O D E  T Y P E  _ G E T */
		/*----------------------------------------------------------------------------
			%%Function: NodeType_get
			%%Qualified: md.WordMLNode.NodeType_get
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public XmlNodeType NodeType
		{
			get
			{
				return m_node.NodeType;
			}
		}

		/* C L O N E  N O D E */
		/*----------------------------------------------------------------------------
			%%Function: CloneNode
			%%Qualified: md.WordMLNode.CloneNode
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNode CloneNode(bool deep)
		{
			return new WordMLNode(m_node.CloneNode(deep), m_nsmgr);
		}

		/* L O C A L  N A M E  _ G E T */
		/*----------------------------------------------------------------------------
			%%Function: LocalName_get
			%%Qualified: md.WordMLNode.LocalName_get
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public string LocalName
		{
			get
			{
				return m_node.LocalName;
			}
		}

		/* W R I T E  T O */
		/*----------------------------------------------------------------------------
			%%Function: WriteTo
			%%Qualified: md.WordMLNode.WriteTo
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public void WriteTo(XmlWriter w)
		{
			m_node.WriteTo(w);
		}

		/* W R I T E  C O N T E N T  T O */
		/*----------------------------------------------------------------------------
			%%Function: WriteContentTo
			%%Qualified: md.WordMLNode.WriteContentTo
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public void WriteContentTo(XmlWriter w)
		{
			m_node.WriteContentTo(w);
		}

		/* P A R E N T  N O D E  _ G E T */
		/*----------------------------------------------------------------------------
			%%Function: ParentNode_get
			%%Qualified: md.WordMLNode.ParentNode_get
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNode ParentNode
		{
			get
			{
				return new WordMLNode(m_node.ParentNode, m_nsmgr);
			}
		}


		/* S E L E C T  S I N G L E  N O D E */
		/*----------------------------------------------------------------------------
			%%Function: SelectSingleNode
			%%Qualified: md.WordMLNode.SelectSingleNode
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNode SelectSingleNode(string sXPath)
		{
			XmlNode node = m_node.SelectSingleNode(sXPath, m_nsmgr);

			return new WordMLNode(node, m_nsmgr);
		}

		/* S E L E C T  N O D E S */
		/*----------------------------------------------------------------------------
			%%Function: SelectNodes
			%%Qualified: md.WordMLNode.SelectNodes
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNodeList SelectNodes(string sXPath)
		{
			XmlNodeList nodes = m_node.SelectNodes(sXPath, m_nsmgr);

			return new WordMLNodeList(nodes, m_nsmgr);
		}

		public void InsertBefore(WordMLNode nodeNew, WordMLNode nodeRef)
		{
			m_node.InsertBefore(nodeNew.InternalNode, nodeRef.InternalNode);
		}

		public void RemoveChild(WordMLNode nodeChild)
		{
			m_node.RemoveChild(nodeChild.InternalNode);
		}

	}

	public class WordMLDocument // WordML Document
	{
		public const string sUriWordML = "http://schemas.microsoft.com/office/word/2003/wordml";

		XmlNamespaceManager m_nsmgr;
		XmlDocument m_dom;

		/* D O M  O P E N  W O R D  M  L */
		/*----------------------------------------------------------------------------
			%%Function: DomOpenWordML
			%%Qualified: md.WordMLDocument.DomOpenWordML
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		private XmlDocument DomOpenWordML(string sFile, out XmlNamespaceManager nsmgr, string sPrefix)
		{
			XmlDocument dom = new XmlDocument();

			dom.Load(sFile);
			nsmgr = new XmlNamespaceManager(dom.NameTable);
			nsmgr.AddNamespace(sPrefix, sUriWordML);

			return dom;
		}

		/* W O R D  M  L  D O C U M E N T */
		/*----------------------------------------------------------------------------
			%%Function: WordMLDocument
			%%Qualified: md.WordMLDocument.WordMLDocument
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLDocument(string sFile, string sPrefix)
		{
			m_dom = DomOpenWordML(sFile, out m_nsmgr, sPrefix);
		}

		public WordMLDocument(XmlDocument dom)
		{
			m_dom = dom;
		}

		/* S E L E C T  N O D E S */
		/*----------------------------------------------------------------------------
			%%Function: SelectNodes
			%%Qualified: md.WordMLDocument.SelectNodes
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNodeList SelectNodes(string sXPath)
		{
			return new WordMLNodeList(m_dom.SelectNodes(sXPath, m_nsmgr), m_nsmgr);
		}

		/* S E L E C T  S I N G L E  N O D E */
		/*----------------------------------------------------------------------------
			%%Function: SelectSingleNode
			%%Qualified: md.WordMLDocument.SelectSingleNode
			%%Contact: rlittle

			
		----------------------------------------------------------------------------*/
		public WordMLNode SelectSingleNode(string sXPath)
		{
			return new WordMLNode(m_dom.SelectSingleNode(sXPath, m_nsmgr), m_nsmgr);
		}


		public void Save(string sFile)
		{
			m_dom.Save(sFile);
		}

		public WordMLNode ImportNode(WordMLNode node, bool fDeep)
		{
			return new WordMLNode(m_dom.ImportNode(node.InternalNode, fDeep), m_nsmgr);
		}
	}

}
