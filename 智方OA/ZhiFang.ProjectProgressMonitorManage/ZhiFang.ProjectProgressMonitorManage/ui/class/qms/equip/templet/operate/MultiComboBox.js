/**
 * 下拉多选带checkbox
 * @author liangyl
 * @version 2017-11-16
 */
Ext.define('Shell.class.qms.equip.templet.operate.MultiComboBox', {
    extend: 'Shell.ux.form.field.SimpleComboBox',  
    alias: 'widget.multicombobox',  
    xtype: 'multicombobox',  
    initComponent: function(){  
        this.multiSelect = true;  
        this.listConfig = {  
              itemTpl : Ext.create('Ext.XTemplate',  
                    '<input type=checkbox>{value}'),  
              onItemSelect: function(record) {      
                  var node = this.getNode(record);  
                  if (node) {  
                     Ext.fly(node).addCls(this.selectedItemCls);  
                       
                     var checkboxs = node.getElementsByTagName("input");  
                     if(checkboxs!=null)  
                     {  
                         var checkbox = checkboxs[0];  
                         checkbox.checked = true;  
                     }  
                  }  
              },  
              listeners:{  
                  itemclick:function(view, record, item, index, e, eOpts ){  
                      var isSelected = view.isSelected(item);  
                      var checkboxs = item.getElementsByTagName("input");  
                      if(checkboxs!=null)  
                      {  
                          var checkbox = checkboxs[0];  
                          if(!isSelected)  
                          {  
                              checkbox.checked = true;  
                          }else{  
                              checkbox.checked = false;  
                          }  
                      }  
                  }  
              }         
        }         
        this.callParent();  
    }  
});  
