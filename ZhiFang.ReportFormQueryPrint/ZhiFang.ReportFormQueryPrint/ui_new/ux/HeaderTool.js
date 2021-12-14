Ext.define('Shell.ux.HeaderTool',{
	extend:'Ext.panel.Tool',
	alias:'widget.uxtool',
	
	renderTpl:[
        '<img id="{id}-toolEl" src="{blank}" class="button-{type}" role="presentation"/>'
    ]
});