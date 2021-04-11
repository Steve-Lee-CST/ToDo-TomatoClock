import os
import re

# convert create_table_sql to .cs
# it is a simple convertor, and the result need to correct manually.
class sql_to_cs:
    @classmethod
    def _type_translator(cls):
        return {
            'INTEGER': 'Int64',
            'TEXT': 'String',
        }

    @classmethod
    def _class_name_translator(cls):
        return {
            'assignments': 'Assignment',
            'capabilities': 'Capability',
            'folder_import_metadata': sql_to_cs._name_to_pascal_name('folder_import_metadata'),
            'groups': 'Group',
            'linked_entities': sql_to_cs._name_to_pascal_name('linked_entity'),
            'members': 'Member',
            'settings': 'Setting',
            'steps': 'Step',
            'task_folders': sql_to_cs._name_to_pascal_name('task_folder'),
            'tasks': 'Task',
        }
    
    @classmethod
    def _name_to_pascal_name(cls, name):
        name_arr = name.split('_')
        pascal_name_arr = [ item.capitalize() for item in name_arr ]
        return ''.join(pascal_name_arr)
    
    def __init__(self, sql_file_path):
        self.sql_file_path = sql_file_path
        # property: { 'col_name': '', 'col_type': '', 'is_unique': False, 'default_value': '', 'prop_name': '', 'prop_type': '' }
        self.info = {
            'table_name': '',
            'class_name': '',
            'properties': [],
        }

    def _line_to_property(self, line):
        # property: { 'col_name': '', 'col_type': '', 'is_unique': False, 'default_value': '', 'prop_name': '', 'prop_type': '' }
        line_arr = line.strip().strip(',').strip().replace('  ', ' ').split(' ')
        if 'PRIMARY KEY' in line:
            return {
                'col_name': '_id', 
                'col_type': line_arr[1], 
                'is_unique': False, 
                'default_value': '', 
                'prop_name': 'Id', 
                'prop_type': sql_to_cs._type_translator()[line_arr[1]],
            }
            
        if len(line_arr) >= 3:
            if line_arr[2] == 'UNIQUE':
                return {
                    'col_name': line_arr[0], 
                    'col_type': line_arr[1], 
                    'is_unique': True, 
                    'default_value': '', 
                    'prop_name': sql_to_cs._name_to_pascal_name(line_arr[0]), 
                    'prop_type': sql_to_cs._type_translator()[line_arr[1]],
                }
            else:
                mj = re.match(r'DEFAULT\((.+)\)', ''.join(line_arr[2:]))
                return {
                    'col_name': line_arr[0], 
                    'col_type': line_arr[1], 
                    'is_unique': False, 
                    'default_value': '' if not mj else mj.group(1).replace('\'', '"'), 
                    'prop_name': sql_to_cs._name_to_pascal_name(line_arr[0]), 
                    'prop_type': sql_to_cs._type_translator()[line_arr[1]],
                }
        
        return {
            'col_name': line_arr[0], 
            'col_type': line_arr[1], 
            'is_unique': False, 
            'default_value': '', 
            'prop_name': sql_to_cs._name_to_pascal_name(line_arr[0]), 
            'prop_type': sql_to_cs._type_translator()[line_arr[1]],
        }

    def _property_to_str(self, prop):
        if prop['default_value']:
            return f'''
        [Column("{prop['col_name']}"), DefaultValue({prop['default_value']})]
        public {prop['prop_type']} {prop['prop_name']} {{ get; set; }}
        '''
        else:
            return f'''
        [Column("{prop['col_name']}")]
        public {prop['prop_type']} {prop['prop_name']} {{ get; set; }}
        '''

    def _class_to_str(self):
        prop_strs = ''.join([ self._property_to_str(prop) for prop in self.info['properties'] ])
        return f'''
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSToDoDB.Modules
{{
    [Table("{self.info['table_name']}")]
    public class {self.info['class_name']} {{
        {prop_strs}
    }}
}}

'''

    def convert(self, save_path):
        with open(self.sql_file_path, 'r') as f:
            self.info['table_name'] = f.readline().strip('(').strip().split(' ')[2]
            self.info['class_name'] = sql_to_cs._class_name_translator()[self.info['table_name']]
            for line in f:
                if line.strip() != ')':
                    self.info['properties'].append(self._line_to_property(line))

        with open(os.path.join(save_path, f'{self.info["class_name"]}.cs'), 'w') as f:
            f.write(self._class_to_str())

if __name__ == '__main__':
    for root,dirs,files in os.walk('sql'): 
        for file in files:
            stc = sql_to_cs(os.path.join('sql', file))
            stc.convert('res')


        

