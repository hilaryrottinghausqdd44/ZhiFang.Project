IF COL_LENGTH('Department', 'cname') IS NOT NULL ALTER TABLE Department ALTER COLUMN cname NVARCHAR(50); 
                
 IF COL_LENGTH('Department', 'shortname') IS NOT NULL ALTER TABLE Department ALTER COLUMN shortname NVARCHAR(50); 
                
IF COL_LENGTH('Department', 'shortcode') IS NOT NULL ALTER TABLE Department ALTER COLUMN shortcode NVARCHAR(50); 