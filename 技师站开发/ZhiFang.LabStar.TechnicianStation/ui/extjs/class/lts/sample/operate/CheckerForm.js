/**
 * 检验审核人设置
 * @author Jcall
 * @version 2020-08-18
 */
Ext.define('Shell.class.lts.sample.operate.CheckerForm', {
	extend:'Shell.class.lts.sample.operate.basic.Form',
	title:'检验审核人设置',
	
	//检验确认人弹出Handler,审核人弹出Checker,保存到内存时用
	OperateType:'Checker',
	//授权操作类型枚举,检验确认人的Name='审核'
	OperateTypeText:'检验确认',
	//授权操作类型枚举，检验确认人的Id='1'
	OperateTypeID:'1',
	//检验小组
	SectionID:null
});