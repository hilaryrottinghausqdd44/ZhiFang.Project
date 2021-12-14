/**
 * 任务一审
 * @author liangyl
 * @version 2017-06-05
 */
Ext.define('Shell.class.wfm.task.new.oneaudit.App',{
    extend:'Shell.class.wfm.task.new.basic.App',
    
    title:'任务一审',
    
    width:600,
    height:400,
      /**字段名列表*/
    FieldList:['','MTypeID','PTypeID','TypeID'],
    /**层次:任务类型(0),开发(1),OA(2),考勤管理(3),默认：0*/
    LEVEL:'0',
    /**字典树IDS*/
    IDS:'',
    /**错误信息样式*/
	errorFormat:'<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**是否存在错误*/
	isError:false,
    createItems: function() {
		var me = this;
		me.IDS = me.IDS || JShell.WFM.GUID.DictTree.TaskType.GUID;
		
		if(!me.LEVEL || !me.IDS){
			me.isError = true;
			me.html = me.errorFormat.replace(/{msg}/g,"请设置LEVEL和IDS参数！");
			return [];
		}
		var lev = me.LEVEL + '';
		if(lev != '0' && lev != '1' && lev != '2' && lev != '3'){
			me.isError = true;
			me.html = me.errorFormat.replace(/{msg}/g,"LEVEL参数的可选值：0,1,2,3</br>现在的参数：" + me.LEVEL);
			return [];
		}
		
		me.Tree = Ext.create('Shell.class.sysbase.dicttree.Tree', {
			region: 'west',
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true,
			collapseMode:'mini',
			rootVisible: false,
			IDS:me.IDS//字典树IDS
		});
		me.Grid = Ext.create('Shell.class.wfm.task.new.oneaudit.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.Tree, me.Grid];
	},
	/**选一行处理*/
	selectOneRow: function(record) {
		var me = this;
		JShell.Action.delay(function() {
            me.Grid.loadTaskTypeData(me.treeIdStr);
        },300);
	}
});