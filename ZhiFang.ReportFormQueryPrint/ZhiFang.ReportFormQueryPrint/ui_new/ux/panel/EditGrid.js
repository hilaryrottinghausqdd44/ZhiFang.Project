/**
 * 列表类
 * @author Jcall
 * @version 2014-08-08
 */
Ext.define('Shell.ux.panel.EditGrid',{
	extend:'Shell.ux.panel.Grid',
	alias:'widget.uxeditgrid',
	
	plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1})
});