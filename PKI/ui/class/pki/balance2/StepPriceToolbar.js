/**
 * 阶梯价格计算查询条件
 * @author liangyl	
 * @version 2017-08-07
 */
Ext.define('Shell.class.pki.balance2.StepPriceToolbar', {
	extend: 'Shell.class.pki.balance2.SearchToolbar',
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			radio1 = me.getComponent('radio1'),
			radio2 = me.getComponent('radio2');
    
		radio1.on({
			change: function(field, newValue) {
				if(newValue) {
					me.changeDateType(1);
				}
			}
		});
		radio2.on({
			change: function(field, newValue) {
				if(newValue) {
					me.changeDateType(2);
				}
			}
		});
		
		var	Dealer_Name = me.getComponent('Dealer_Name'),
	    Dealer_Id = me.getComponent('Dealer_Id');
		Dealer_Name.on({
			check:function(p,record){
				//选择所有经销商，id和name 为空
				var idval=record ? record.get('tid') : '';
				var nameval=record ? record.get('text') : '';
				if(idval=='0' ||  idval==0 ){
					idval='';
					nameval='';
				}
				Dealer_Id.setValue(idval);
				Dealer_Name.setValue(nameval);
				p.close();
			}
		});

		var checkList = ['TestItem_CName'];
		for(var i in checkList) {
			me.initCheckTriggerListeners(checkList[i]);
		}
	},
		/**清空查询内容*/
	onClearSearch: function() {
		var me = this,
			DateType = me.getComponent('DateType'),
			radio2 = me.getComponent('radio2'),
			BeginDate = me.getComponent('BeginDate'),
			EndDate = me.getComponent('EndDate'),
			YearMonth = me.getComponent('YearMonth'),
			MonthMonth = me.getComponent('MonthMonth');

		var checkList = [
			 'TestItem_CName','Dealer_Name'
		];
		for(var i in checkList) {
			var check = me.getComponent(checkList[i]);
			var check_Id = me.getComponent(checkList[i].split('_')[0] + '_Id');
			if(check) check.setValue('');
			if(check_Id) check_Id.setValue('');
		}

		if(DateType.getValue() != me.defaultDateTypeValue) {
			DateType.setValue(me.defaultDateTypeValue);
		}

		radio2.setValue(true);
	}
});