/**
 * OA微信公众号
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.weixin.PublicNum',{
	extend:'Ext.panel.Panel',
    title:'OA微信公众号',
    bodyPadding:5,
    width:160,
    height:180,
	html:
	'<div style="text-align:center;font-weight:bold;">' +
		'<span>请扫描下方二维码</span></br>' +
		'<img style="width:128px;height:128px;" src="' + 
			JShell.System.Path.UI + '/images/wfm/oa_barcord.jpg">' +
	'</div>'
});