using System;
using System.IO;
using System.Text;
using NWN;

namespace HakInstaller
{
	/// <summary>
	/// Summary description for PRCHif.
	/// </summary>
	public class PRCHif
	{
		/// <summary>
		/// Gets the file name of the PRC hif.
		/// </summary>
		public static string PRCHifFileName { get { return "PRC Consortium Pack.hif"; } }

		/// <summary>
		/// Gets the full path to the PRC hif.
		/// </summary>
		public static string PRCHifFullPath { get { return NWNInfo.GetFullFilePath(PRCHifFileName); } }

		/// <summary>
		/// Creates a temporary HIF file on disk for the PRC pack.
		/// </summary>
		public static void CreatePRCHif()
		{
			using (StreamWriter writer = new StreamWriter(PRCHifFullPath, false, Encoding.ASCII))
			{
				writer.Write(HIF);
			}
		}

		/// <summary>
		/// String constant for the PRC hif, the PRC version of the installer uses
		/// this as its HIF instead of looking for HIF files.
		/// </summary>
		private const string HIF = 
			"erf : prc_consortium.erf\r\n" +
			"module.Hak : prc_consortium.hak\r\n" +
			"module.CustomTlk : prc_consortium.tlk\r\n" +

			"module.OnClientEnter : prc_onenter\r\n" +
			"module.OnPlayerLevelUp : prc_levelup\r\n" +
			"module.OnPlayerEquipItem : prc_equip\r\n" +
			"module.OnPlayerUnequipItem : prc_unequip\r\n" +
			"module.OnPlayerRest : prc_rest\r\n" +

			"module.Cache : screen_targets\r\n" +
			"module.Cache : prc_caster_level\r\n" +
			"module.Cache : set_damage_type\r\n" +
			"module.Cache : change_metamagic\r\n" +
			"module.Cache : add_spell_dc\r\n" +
			"module.Cache : add_spell_penetr\r\n" +
			"module.Cache : add_damage\r\n" +
			"module.Cache : add_damageshield\r\n" +
			"module.Cache : add_randdamage\r\n" +
			"module.Cache : add_healing\r\n" +
			"module.Cache : add_hitpoints\r\n";
	}
}
