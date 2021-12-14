/**
 * 检验确认人设置
 * @author Jcall
 * @version 2020-08-18
 */
Ext.define('Shell.class.lts.sample.operate.HandlerForm', {
	extend:'Shell.class.lts.sample.operate.basic.Form',
	title:'检验确认人设置',
	
	//检验确认人弹出Handler,审核人弹出Checker,保存到内存时用
	OperateType:'Handler',
	//授权操作类型枚举,检验确认人的Name='检验确认'
	OperateTypeText:'检验确认',
	//授权操作类型枚举，检验确认人的Id='2'
	OperateTypeID:'2',
	//检验小组
	SectionID:null
});