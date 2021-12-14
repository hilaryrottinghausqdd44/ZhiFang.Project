/**
 * 首页Home基础面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.main.home.Table',{
    extend:'Shell.class.sysbase.main.home.Basic',
    layout:{
        type:'table',
        columns:3,
        tableAttrs: {
            style: {
                width: '100%'
            }
        }
    },
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 初始化组件类数据*/
	initClassListData:function(callback){
		var me = this,
			items = [];
			
		items.push({
			className:'Shell.class.wfm.weixin.PublicNum',
			classConfig:{
				title:'微信公众号',
				colspan:3
			}
		},{
			className:'Shell.class.wfm.task2.execute.small.Grid',
			classConfig:{
				title:'执行任务1',
				colspan:2
			}
		},{
			className:'Shell.class.wfm.task2.execute.small.Grid',
			classConfig:{
				title:'执行任务2',
				colspan:1
			}
		},{
			className:'Shell.class.wfm.task2.execute.small.Grid',
			classConfig:{
				title:'执行任务3',
				colspan:3
			}
		});
		
		me.CLASS_LIST = items;
		
		callback();
	}
});