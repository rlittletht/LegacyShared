using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace Opts
{
class XmlOptions
{

	private XmlDocument m_dom;
	private XmlNamespaceManager m_nsmgr;
	private string m_sUri;

	~XmlOptions()
	{
		if (m_sw != null)
			throw new Exception("didn't call XmlOptions.Close() before destructor called!");
	}

	public XmlOptions(string sUri)
	{
		m_sUri = sUri;
		m_dom = new XmlDocument();
		m_nsmgr = new XmlNamespaceManager(m_dom.NameTable);

		m_nsmgr.AddNamespace("c", sUri);
	}

	public bool FLoad(string sConfigFile)
	{
		string s;

		if (Path.IsPathRooted(sConfigFile))
			s = sConfigFile;
		else
			{
			string sFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			s = sFolder + "\\" + sConfigFile;
			}

		if (File.Exists(s))
			{
			m_dom.Load(s);
			return true;
			}
		return false;
	}

	StreamWriter m_sw;
	string m_sPrefix;

	public void CreateOnStream(StreamWriter sw)
	{
		m_sw = sw;

		m_sPrefix = "x";
		m_sw.WriteLine("<" + m_sPrefix + ":options xmlns:" + m_sPrefix + "='" + m_sUri + "'>");
	}

	public void Create(string sFolderFile, string sConfigFile)
	{
		string sFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + sFolderFile;
		string s = sFolder + "\\" + sConfigFile;

		if (!Directory.Exists(sFolder))
			Directory.CreateDirectory(sFolder);

		StreamWriter sw = File.CreateText(s);
		CreateOnStream(sw);
	}

	public void DetachFromStream()
	{
		m_sw.WriteLine("</" + m_sPrefix + ":options>");
		m_sw = null;
	}

	public void Close()
	{
		StreamWriter sw = m_sw;
		DetachFromStream();
		sw.Close();
		sw = null;
	}

		
	/* N O D E  F O R  O P T I O N */
	/*----------------------------------------------------------------------------
		%%Function: NodeForOption
		%%Qualified: pex1.PEXO.NodeForOption
		%%Contact: rlittle

		
	----------------------------------------------------------------------------*/
	XmlNode NodeForOption(string sOption)
	{
		if (m_dom != null && m_nsmgr != null)
			{
			string sPrefix = m_nsmgr.LookupPrefix(m_sUri);

			XmlNode node = m_dom.SelectSingleNode(String.Format("//{0}:options/{0}:{1}/@val", sPrefix, sOption), m_nsmgr);
			return node;
			}
		return null;
	}

	XmlNodeList NodesForOption(string sOption)
	{
		if (m_dom != null && m_nsmgr != null)
			{
			string sPrefix = m_nsmgr.LookupPrefix(m_sUri);

			XmlNodeList nodes = m_dom.SelectNodes(String.Format("//{0}:options/{0}:{1}/@val", sPrefix, sOption), m_nsmgr);
			return nodes;
			}
		return null;
	}

	/* R E A D  O P T I O N */
	/*----------------------------------------------------------------------------
		%%Function: ReadOption
		%%Qualified: pex1.PEXO.ReadOption
		%%Contact: rlittle

	----------------------------------------------------------------------------*/
	public void ReadOption(string sOption, ref int nOption, int nDefault)
	{
		XmlNode node = NodeForOption(sOption);

		if (node != null)
			nOption = Int32.Parse(node.Value);
		else
			nOption = nDefault;
	}

	public void ReadOption(string sOption, ref bool fOption, bool fDefault)
	{
		XmlNode node = NodeForOption(sOption);

		if (node != null)
			{
			if (String.Compare((string)node.Value, "true", true) == 0)
				fOption = true;
			else if (String.Compare((string)node.Value, "false", true) == 0)
				fOption = false;
			else
				fOption = fDefault;
			}
		else
			fOption = fDefault;
	}

	public void ReadOption(string sOption, ref string sValue, string sDefault)
	{
		XmlNode node = NodeForOption(sOption);

		if (node != null)
			sValue = (string)node.Value;
		else
			sValue = sDefault;
	}

	public void ReadOption(string sOption, ref List<string> plsValue, List<string> plsDefault)
	{
		XmlNodeList nodes = NodesForOption(sOption);

		if (nodes != null)
			{
			plsValue = new List<string>();
			foreach(XmlNode node in nodes)
				{
                string sValue = (string)node.Value;
				plsValue.Add(sValue);
				}
			}
		else
			plsValue = plsDefault;
	}

	/* W R I T E  O P T I O N */
	/*----------------------------------------------------------------------------
		%%Function: WriteOption
		%%Qualified: pex1.PEXO.WriteOption
		%%Contact: rlittle

	----------------------------------------------------------------------------*/
	public void WriteOption(string sElt, int val)
	{
		m_sw.WriteLine("<" + m_sPrefix + ":" + sElt + " val='" + val.ToString()+"'/>");
	}

	public void WriteOption(string sElt, string sVal)
	{
		m_sw.WriteLine("<" + m_sPrefix + ":" + sElt + " val='" + sVal+"'/>");
	}

	public void WriteOption(string sElt, bool fVal)
	{
		m_sw.WriteLine("<" + m_sPrefix + ":" + sElt + " val='" + fVal.ToString()+"'/>");
	}

	public void WriteOption(string sElt, List<string> plsSearchTags)
	{
		if (plsSearchTags == null)
			return;

		foreach(string s in plsSearchTags)
			{
			WriteOption(sElt, s);
			}
	}
}
}

