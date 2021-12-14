/**
 * 严格流程--文档起草应用
 * @author longfc
 * @version 2017-05-15
 */
Ext.define('Shell.class.qms.file.file.strict.draft.App', {
	extend: 'Shell.class.qms.file.file.create.CreateApp',
	title: '文档起草',
	
	/**
	 * 功能按钮是否隐藏:组件是否隐藏,只起草,仅审核,仅批准,自动发布
	 * 第一个参数为功能按钮是否显示或隐藏
	 * 第二个参数为只起草选择项是否显示或隐藏
	 * 第三个参数为仅审核选择项是否显示或隐藏
	 * 第四个参数为仅批准选择项是否显示或隐藏
	 * 第五个参数为发布选择项是否显示或隐藏
	 * */
	hiddenRadiogroupChoose: [false, false, true, true, true]
});