/**
 * PKI参数设置
 * @author Jcall
 * @version 2015-09-10
 */

var JcallShell = JcallShell || {};

JcallShell.System = JcallShell.System || {};

/**系统信息*/
JcallShell.System.Name = '报告程序打印系统';

JcallShell.PRI = JcallShell.PRI || {};

JcallShell.PRI.System  = {
	Path:{
		REPORT:'ReportFiles'//报告文件目录
	}
};

(function() {
	window.JShell = JcallShell;
	//语言包处理，默认加载中文语言包
	var params = JShell.Page.getParams(true);
	if(params.LANG){
		JcallShell.System.Lang = params.LANG;
	}
	//加载语言
	JcallShell.Page.changeLangage(JcallShell.System.Lang);
})();