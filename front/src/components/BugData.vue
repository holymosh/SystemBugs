<template>
  <v-card>
    <v-card-title>
      Data table
      <v-spacer></v-spacer>
      <v-text-field
        v-model="search"
        append-icon="mdi-magnify"
        label="Search"
        single-line
        hide-details
      ></v-text-field>
    </v-card-title>
    <v-data-table
      :headers="headers"
      :items="tableData"
      :search="search"
      :items-per-page="5"
    ></v-data-table>
  </v-card>
</template>

<script>
export default {
  data () {
    return {
      search: '',
      headers: [
        {
          text: 'Id',
          align: 'start',
          value: 'id'
        },
        { text: 'Система', value: 'system' },
        { text: 'Описание', value: 'summary' },
        { text: 'Состояние', value: 'state' },
        { text: 'Критичность', value: 'criticalLevel' },
        { text: 'Тип дефекта', value: 'defectType' },
        { text: 'Создано', value: 'creationDate' },
        { text: 'Изменено', value: 'changeDate' },
        { text: 'Закрыто', value: 'closingDate' },
        { text: 'Обнаружения', value: 'reopnesAmount' }
      ],
      tableData: []
    }
  }
}
</script>
