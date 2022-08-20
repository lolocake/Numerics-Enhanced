﻿
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	public class DataStore
	{
		public static readonly string[] TaskNames = {
			"Write document", 
			"Research", 
			"Google search", 
			"Develop plan",
			"Consulting", 
			"Buget effort", 
			"Master plan", 
			"Management",
			"Project management", 
			"Testing", 
			"Inventarization", 
			"Create slides",
			"Documentation",
			"Travelling", 
			"Stock update", 
			"Literature",
			"Getting sample data",
			"Conference", 
			"Reading",
			"Summarizing",
			"Team event", 
			"Integration",
			"Planning",
			"Copyright check", 
			"Trademark research", 
			"Sales planning",
			"Time management", 
			"Team event",
			"Scrum meeting",
			"Sales pitch",
			"Budgetting",
		};
		public static readonly string[,] SalesTerms = { {"Absorbed Business"," A company that has been merged into another company."},
                                                {"Absorbed costs","The indirect costs associated with manufacturing, for example, insurance or property taxes."},
                                                {"Account balance","The difference between the debit and the credit sides of an account."},
                                                {"Accrual basis","A method of keeping accounts that shows expenses incurred and income  earned for a given fiscal period, even though such expenses and income have not been actually paid or received in cash."},
                                                {"Amortization"," To liquidate on an installment basis; the process of grad­ually paying off a liability over a period of time."},
                                                {"Audiotaping"," The act of recording onto an audiotape."},
                                                {"Back-to-back loan","an arrangement in which two companies in different countries borrow offsetting amounts in each other's currency and each repays it at a specified future date in its domestic currency. Such a loan, often between a company and its foreign subsidiary, eliminates the risk of loss from exchange rate fluctuations."},
                                                {"Back pay","pay that is owed to an employee for work carried out before the current payment period and is either overdue or results from a backdated pay increase.  "},
                                                {"Ballpark"," an informal term for a rough, estimated figure."},
                                                {"Bill of sale","Formal legal document that conveys title to or interest in specific property from the seller to the buyer."},
                                                {"Business  venture"," Taking financial risks in a commercial enterprise"},
                                                {"Seed money"," a usually modest amount of money used to convert an idea into a viable business. Seed money is a form of venture capital."},
                                                {"Settlement","the payment of a debt or charge."},
                                                {"Sole proprietorship","Business legal structure in which one individual owns the business."},
                                                {"Venture funding","the round of funding for a new company that follows seed funding provided by venture capitalists.    "},
                                                {"Viral marketing","the rapid spread of a message about a new product or service in a similar way to the spread of a virus  "},
                                                {"Vulture capitalist","a venture capitalist who structures deals on behalf of an entrepreneur in such a way that the investors benefit rather than the entrepreneur"},
                                                {"Wallet technology","a software package providing digital wallets or purses on the computers of merchants and customers to facilitate payment by digital cash"},
                                                {"Whistleblowing"," speaking out to the media or the public on malpractice, misconduct, corruption, or mismanagement witnessed in an organization"},
                                                {"Withholding tax","the money that an employer pays directly to the U.S. government as a payment of the income tax on the employee "},
                                                {"Patent","a type of copyright granted as a fixed-term monopoly to an inventor by the state to prevent others copying an invention, or improvement  of a product or process"},
                                                {"Point of purchase","the place at which a product is purchased by the customer. The point of sale can be a retail outlet, a display case, or even a  legal business relationship of two or more people who share responsibilities, resources, profits and liabilities."},
                                                {"Price floor","The lowest amount a business owner can charge for a prod­uct or service and still meet all expenses"},
                                                {"Product mix","All of the products in a seller's total product line."},
                                                {"Psychographics","The system of explaining market behavior in terms of attitudes and life styles."},
                                                {"Added value ","the element(s) of service or product that a sales person or selling organization provides, that a customer is prepared to pay for because of the benefit(s) obtained. "},
                                                {"Advantage","the aspect of a product or service that makes it better than another, especially the one in-situ or that of a competitor"},
                                                {"Benefit","the gain (usually a tangible cost, but can be intangible) that accrues to the customer from the product or service."},
                                                {"Buying warmth","behavioural, non-verbal and other signs that a prospect likes what he sees; very positive from the sales person's perspective, but not an invitation to jump straight to the close"},
                                                {"Commodities","typically a term applied to describe products which are mature in development, produced and sold in vast scale, involving little or no uniqueness between variations of different suppliers; high volume, low price, low profit margin, de-skilled ('ease of use' in consumption, application, installation, etc)."},
                                                {"Deal ","common business parlance for the sale or purchase (agreement or arrangement). It is rather a colloquial term so avoid using it in serious company as it can sound flippant and unprofessional."},
                                                {"Deliverable","an aspect of a proposal that the provider commits to do or supply, usually and preferably clearly measurable."},
                                                {"Influencer ","a person in the prospect organization who has the power to influence and persuade a decision-maker."},
                                                {"Intangible ","in a selling context this describes, or is, an aspect of the product or service offering that has a value but is difficult to see or quantify (for instance, peace-of-mind, reliability, consistency). "},
                                                {"Lead-time","time between order and delivery, installation or commencement of a product or service"},
                                                {"Open plan selling","a modern form of selling, heavily dependent on the sales person understanding and interpreting the prospect's organizational and personal needs, issues, processes, constraints and strategic aims, which generally extends the selling discussion far beyond the obvious product application"},
                                                {"Referral ","a recommendation or personal introduction or permission/suggestion made by someone, commonly but not necessarily a buyer, which enables the seller to approach or begin dialogue with a new perspective buyer or decision"},
                                                {"Sales funnel","describes the pattern, plan or actual achievement of conversion of prospects into sales, pre-enquiry and then through the sales cycle."},
                                           
                                            };
        public static Dictionary<string, string> CommonExtensions = new Dictionary<string, string>
			{
				{"ace", "ACE Archiver compression file."},
				{"aif", "Audio Interchange File used with SGI and Macintosh applications."},
				{"ani", "Animated cursors used in Microsoft Windows."},
				{"api", "Application Program Interface."},
				{"art", "Clipart."},
				{"asc", "ASCII text file."},
				{"asm", "Assembler code."},
				{"asp", "Microsoft Active Server Page."},
				{"avi", "Audio/Video Interleaved used for Windows based movies."},
				{"bak", "Backup Files."},
				{"bas", "BASIC programming language sourcecode."},
				{"bat", "MS-DOS batch file."},
				{"bfc", "Briefcase document used in Windows."},
				{"bin", "Binary File."},
			
				{"bmp", "Bitmap format."},
				{"bud", "Backup Disk for Quicken by Intuit."},
				{"bz2", "Bzip2-compressed files."},
				{"c", "C source file."},
				{"cat", "Security Catalog file."},
				{"cbl", "Cobol code."},
				{"cbt", "Computer Based Training."},
				{"cda", "Compact Disc Audio Track."},
				{"cdt", "Corel Draw Template file."},
				{"cfml", "ColdFusion Markup Language."},
				{"cgi", "Common Gateway Interface. Web based programs and scripts."},
				{"chm", "Compiled HTML Help files used by Windows."},
				{"class", "Javascript Class file."},
				{"clp", "Windows Clipboard file."},
				{"cmd", "Dos Command File."},
				{"com", "Command File."},
				{"cpl", "Control panel item"},

				{"cp", "C++ programming language source code."},
				{"css", "Cascading Style Sheet. Creates a common style reference for a set of web pages."},
				{"csv", "Comma Separated Values format."},
				{"cmf", "Corel Metafile."},
				{"cur", "Cursor in Microsoft Windows."},
				{"dao", "Registry Backup file for Windows registry."},
				{"dat", "Data file."},
				{"dd", "Compressed Archive by Macintosh DiskDoubler."},
				{"deb", "Debian packages."},
				{"dev", "Device Driver."},
				{"dic", "Dictionary file."},
				{"dir", "Macromedia Director file."},
				{"dll", "Dynamic Linked Library. Microsoft application file."},
				{"doc", "Document format for Word Perfect and Microsoft Word."},
				{"dot", "Microsoft Word Template."},
				{"drv", "Device Driver."},
				{"ds", "TWAIN Data source file."},
				{"dun", "Dial-up networking configuration file."},
				{"dwg", "Autocad drawing."},
				{"dxf", "Autocad drawing exchange format file."},
				{"emf", "Enhanced Windows Metafile."},
				{"eml", "Microsoft Outlook e-mail file."},
				{"eps", "Encapsulated PostScript supported by most graphics programs."},
				{"eps2", "Adobe PostScript Level II Encapsulated Postscript."},
				{"exe", "DOS based executable file which is also known as a program."},
				{"ffl", "Microsoft Fast Find file."},
				{"ffo", "Microsoft Fast Find file."},
				{"fla", "Macromedia Flash movie format."},
				{"fnt", "Font file."},

				{"gif", "Graphics Interchange Format that supports animation. Created by CompuServe and used primarily for web use."},
				{"gid", "Windows global index. Contains the index information used by Help in Windows."},
				{"grp", "Microsoft Program Manager Group."},
				{"gz", "Unix compressed file."},
				{"hex", "Macintosh binary hex(binhex) file."},
				{"hlp", "Standard help file."},
				{"ht", "HyperTerminal files."},
				{"hqx", "Macintosh binary hex(binhex) file."},
				{"htm", "Hyper Text Markup. This markup language is used for web design."},
				{"html", "Hyper Text Markup Language. This markup language is used for web design."},
				{"icl", "Icon Library File."},
				{"icm", "Image Color Matching profile."},
				{"ico", "Microsoft icon image."},
				{"inf", "Information file used in Windows."},
				{"ini", "Initialization file used in Windows."},
				{"jar", "Java Archive. A compressed java file format."},
				{"jpeg", "Compression scheme supported by most graphics programs and used predominantly for web use."},
				{"jpg", "More common extension for JPEG described above."},
				{"js", "JavaScript File - A text file containing JavaScript programming code."},
				{"lab", "Microsoft Excel mailing labels."},
				{"lit", "eBooks in Microsoft Reader format."},
				{"lnk", "Windows 9x shortcut file."},
				{"log", "Application log file."},
				{"lsp", "Autocad(visual) lisp program."},
				{"maq", "Microsoft Access Query."},

				{"mar", "Microsoft Access Report."},
				{"mdb", "Microsoft Access DataBase File."},
				{"mdl", "Rose model file. Opens with Visual Modeler or Rational Rose."},
				{"mid", "MIDI music file."},
				{"mod", "Microsoft Windows 9.x kernel module."},
				{"mov", "Quicktime movie."},
				{"mp3", "MPEG Audio Layer 3."},
				{"mpeg", "Animation file format."},
				{"mpp", "Microsoft Project File."},
				{"msg", "Microsoft Outlook message file."},
			
				{"ncf", "Netware command File."},
				{"nlm", "Netware loadable Module."},
				{"o", "Object file, used by linkers."},
				{"ocx", "ActiveX Control: A component of the Windows environment."},
				{"ogg", "Ogg Vorbis digitally encoded music file."},
				{"ost", "Microsoft Exchange/Outlook offline file."},
				{"pak", "WAD file that contains information about levels, settings, maps, etc for Quake and Doom."},
				{"pcl", "Printer Control Language file. PCL is a Page Description Language developed by HP."},
				{"pct", "Macintosh drawing format."},
				{"pdf", "Portable Document File by Adobe. "},
			
				{"pdr", "Port driver for windows 95. It is actually a virtual device driver (vxd)."},
				{"php", "Web page that contains a PHP script."},
				{"phtml", "Web page that contains a PHP script."},
			
			
				{"pif", "Program Information File"},
				{"pl", "Perl source code file."},
				{"pm", "Perl Module."},
				{"png", "Portable Network Graphic file."},
				{"pol", "System Policy file for Windows NT."},
				{"pot", "Microsoft PowerPoint design template."},
				{"pps", "Microsoft PowerPoint slide show."},
				{"ppt", "Microsoft PowerPoint presentation(default extension)."},
				{"prn", "A print file created as the result of printing to file."},
				{"ps", "PostScript file."},
				{"psd", "Native Adobe Photoshop format."},
				{"psp", "Paint Shop Pro image."},
				{"pst", "Personal Folder File for Microsoft Outlook."},
				{"pub", "Microsoft Publisher document."},
				{"qif", "Quicken Import file."},
				{"ram", "RealAudio Metafile."},
				{"rar", "RAR compressed archive created by Eugene Roshall."},
				{"raw", "Raw File Format."},
				{"rdo", "Raster Document Object. Proprietary file type used on Xerox"},
				{"reg", "Registry file that contains registry settings."},
				{"rm", "RealAudio video file."},
				{"rpm", "RedHat Package Manager."},
				{"rsc", "Standard resource file."},
				{"rtf", "Rich Text Format."},
				{"scr", "Screen Saver file."},
				{"sea", "Self-extracting archive for Macintosh Stuffit files."},
				{"sgml", "Standard Generalized Markup Language."},
				{"sh", "Unix shell script."},
				{"shtml", "HTML file that supports Server Side Includes(SSI)."},
				{"sit", "Compressed Macintosh Stuffit files."},
				{"smd", "SEGA mega drive ROM file."},
				{"svg", "Adobe scalable vector graphics file."},
				{"swf", "Shockwave Flash file by Macromedia."},
				{"swp", "DOS swap file."},
				{"sys", "Windows system file used for hardware configuration or drivers."},
				{"tar", "Unix Tape Archive."},
				{"tga", "Targa bitmap."},
				{"tiff", "Tagged Image File Format."},
				{"tmp", "Windows temporary file."},
				{"ttf", "True Type font."},
				{"txt", "Text Format."},
				{"udf", "Uniqueness Definition File. Used for Windows unattended installations."},
				{"uue", "UU-encoded file."},
				{"vbx", "Microsoft Visual basic extension."},
				{"vm", "Virtual Memory file."},			
				{"vxd", "Windows 9x virtual device driver."},
				{"wav", "Waveform sound file."},
				{"wmf", "Windows Metafile (graphics format)."},
				{"xcf", "The GIMP's native image format."},
				{"xif", "Xerox Image file (same as TIFF)."},
				{"xls", "Microsoft Excel Spreadsheet."},
				{"xlt", "Microsoft Excel Template."},
				{"xml", "Extensible markup language."},
				{"xsl", "XML style sheet."},
				{"zip", "Compressed Zip archive."},
			};

        public static Dictionary<string, string> OfficeExtensions = new Dictionary<string, string>
			{
				{"accda","Microsoft Access 2007/2010 add-in file"},
				{"accdb","Microsoft Access 2007/2010 database file"},
				{"accdc","Microsoft Access 2007/2010 digitally signed database file"},
				{"accde","Microsoft Access 2007/2010 compiled execute only file"},
				{"accdp","Microsoft Access 2007/2010 project file"},
				{"accdr","Microsoft Access 2007/2010 runtime mode database file"},
				{"accdt","Microsoft Access 2007/2010 database template file"},
				{"accdu","Microsoft Access 2007/2010 database wizard file"},
				{"acl","Microsoft Office automatic correction list"},
				{"ade","Microsoft Access compiled project file"},
				{"adp","icrosoft Access project file"},
				{"asd","Microsoft Word auto-save document file"},
				{"cnv","Microsoft Word text conversion file"},
				{"doc","Microsoft Word 97 to 2003 document file"},
				{"docm","Microsoft Word 2007/2010 Open XML macro-enabled document file"},
				{"docx","Microsoft Word 2007/2010 Open XML document file"},
				{"dot","Microsoft Word 97 to 2003 document template file"},
				{"dotm","Microsoft Word 2007/ 2010 Open XML macro-enabled document template file"},
				{"dotx","Microsoft Word 2007 or Word 2010 XML document template file"},
				{"grv","Microsoft SharePoint WorkSpace Groove file"},
				{"iaf","Microsoft Outlook exported account and email setting file"},
				{"laccd","Microsoft Access 2007/2010 database lock file"},
				{"maf","Microsoft Access form shortcut file"},
				{"mam","Microsoft Access macro shortcut file"},
				{"maq","Microsoft Access query shortcut file"},
				{"mar","Microsoft Access report shortcut file"},
				{"mat","Microsoft Access table shortcut file"},
				{"mda","Microsoft Access add-in file"},
				{"mdb","Microsoft Access database file"},
				{"mde","Microsoft Access compiled database (application) file"},
				{"mdt","Microsoft Access database template file"},
				{"mdw","Microsoft Access workgroup information file"},
				{"mpd","Microsoft Project database file"},
				{"mpp","Microsoft Project plan file"},
				{"mpt","Microsoft Project template tile"},
				{"oab","Microsoft Outlook offline address book file"},
				{"obi","Microsoft Outlook 2007/2010 RSS subscription file"},
				{"oft","Microsoft Outlook template file"},
				{"olm","Microsoft Outlook for Mac data file"},
				{"one","Microsoft OneNote document file"},
				{"onepk","Microsoft OneNote package file"},
				{"ops","Microsoft Office profile settings file"},
				{"ost","Microsoft Outlook inbox off-line folder file"},
				{"pa","Microsoft PowerPoint add-in file"},
				{"pip","Microsoft Office personalized settings file"},
				{"pot","Microsoft PowerPoint 97 to 2003 template file"},
				{"potm","Microsoft PowerPoint 2007/2010 macro-enabled Open XML template file"},
				{"potx","Microsoft PowerPoint 2007/2010 Open XML presentation template file"},
				{"ppa","Microsoft PowerPoint 97 to 2003 add-in file"},
				{"ppam","Microsoft PowerPoint 2007/2010 macro-enabled Open XML add-in file"},
				{"pps","Microsoft PowerPoint 97 to 2003 complete slide show file"},
				{"ppsm","Microsoft PowerPoint 2007/2010 macro-enabled Open XML complete slide show file"},
				{"ppsx","Microsoft PowerPoint 2007/2010 Open XML complete slide show file"},
				{"ppt","Microsoft PowerPoint 97 to 2003 Presentation file"},
				{"pptm","Microsoft PowerPoint 2007/2010 macro-enabled Open XML presentation file"},
				{"pptx","Microsoft PowerPoint 2007/2010 Open XML presentation file"},
				{"prf","Microsoft Outlook profile file"},
				{"pst","Microsoft Outlook personal folder file"},
				{"pub","Microsoft Publisher document file"},
				{"puz","Microsoft Publisher packed file"},
				{"rpmsg","Microsoft Restricted Permission Message file"},
				{"sldm","Microsoft PowerPoint 2007/2010 macro-enabled Open XML slide file"},
				{"sldx","Microsoft PowerPoint 2007/2010 Open XML slide file"},
				{"slk","Microsoft Symbolic Link format file"},
				{"snp","Microsoft Access report shapshot file"},
				{"svd","Microsoft Word document autosave file"},
				{"thmx","Microsoft Office 2007/2010 theme file"},
				{"vdx","Microsoft Visio drawing XML file"},
				{"vsd","Microsoft Visio diagram document file"},
				{"vss","Microsoft Visio smartshapes file"},
				{"vst","Microsoft Visio flowchart file"},
				{"vsx","Microsoft Visio stencil XML file"},
				{"vtx","Microsoft Visio XML template file"},
				{"wbk","Microsoft Word auto-backup document file"},
				{"wll","Microsoft Word add-in file"},
				{"xar","Microsoft Excel AutoRecover backup file"},
				{"xl","Microsoft Excel spreadsheet file"},
				{"xla","Microsoft Excel add-in file"},
				{"xlam","Microsoft Excel 2007/2010 Open XML macro-enabled add-in file"},
				{"xlb","Microsoft Excel Toolbars file"},
				{"xlc","Microsoft Excel Chart file"},
				{"xll","Microsoft Excel add-in file"},
				{"xlm","Microsoft Excel Macro file"},
	