/*
 * 该颜色选择器，颜色选择框一直显示在下拉框下面
 */
Ext.define('Shell.ux.form.field.ColorField',{
	extend:'Ext.form.field.Picker',
	alias:'widget.colorfield',
	requires:['Ext.picker.Color'],
	mixins:['Shell.ux.Color'],
	triggerTip:'请选择一个颜色',
	//labelWidth:60,
	fieldLabel:'颜色',
	//value:'#FFFFFF',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.value){
			me.changeInputContent(me.value);
		}
		
		me.on({
			specialKey:function(field,e){
				if(e.getKey() == Ext.EventObject.ENTER){
					me.changeInputContent(field.value);
				}
			}
		});
	},
	createPicker:function () {
		var me = this;
		var config = {
			ownerCt:me,
			pickerField:me,
			floating:true,
			hidden:true,
			focusOnShow:true,
			frame:true,
			//minWidth:144,
			listeners:{
				select:function(picker,selColor){
					me.changeInputContent("#"+selColor);
				}
			}
		};
		//自定义颜色选择
		if(Ext.isArray(me.colors)){
			config.colors = me.colors;
		}
		return Ext.create('Ext.picker.Color', config);
	},
    //更改显示内容
    changeInputContent:function(value){
    	var me = this;
    	me.setValue(value);
    	//根据背景色明亮度确定文字颜色黑白
    	var color = me.isLight(value) ? "#000" : "#FFF";
    	me.setFieldStyle('background:' + value + ';color:' + color);
    	return;
    	
//  	if(value.slice(0,1) == '#'){
//  		var selColor = value.slice(1);
//  		// 实现根据选择的颜色来改变背景颜色,根据背景颜色改变字体颜色,防止看不到值
//          var r = parseInt(selColor.substring(0,2),16);
//          var g = parseInt(selColor.substring(2,4),16);
//          var b = parseInt(selColor.substring(4,6),16);
//          var a = new Ext.draw.Color(r,g,b);
//          var l = a.getHSL()[2];
//          if (l > 0.5 || selColor.toLowerCase() === 'ffff00') {
//              me.setFieldStyle('background:#' + selColor + ';color:#000000');
//          }
//          else{
//              me.setFieldStyle('background:#' + selColor + ';color:#FFFFFF');
//          }
//  	}else{
//  		me.setFieldStyle('color:#000000');
//  	}
    }
});