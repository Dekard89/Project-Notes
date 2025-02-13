<script >

import noteForm from "@/components/NoteForm.vue";
import noteComponent from "@/components/NoteComponent.vue";
import modalWindow from "@/components/ModalWindow.vue";
import NoteList from "@/components/NoteList.vue";
import axios from "axios";
export default {
  components: {
    noteComponent,
    noteForm,
    modalWindow,
    NoteList
  },
  data() {
    return {
     /* notes: [{
        id: 1,
        title: 'Test Note',
        description: 'В работе',
        changeTime: '2019-06-01',
        status: 0
      },
        {
          Id: 2,
          Title: 'Test Note2',
          Description: 'Test Description2',
          ChangeTime: '2019-06-01',
          Status: 0
        }],
      notes2: [{
        Id: 3,
        title: 'Test Note3',
        description: 'В работе',
        changeTime: '2019-06-01',
        status: 1
      },
        {
          Id: 4,
          Title: 'Test Note4',
          Description: 'Test Description2',
          ChangeTime: '2019-06-01',
          Status: 1
        }],*/
      modalVisible: false,
      inWorkList: [],
      executeList: [],
      cancelList: [],
      errors:[],
      pageNumber: 1
    }
  },
  methods: {
    async createNote(noteRequest) {
      try{
        await axios.post('/api/notes/create', noteRequest)
            if(this.inWorkList.length > 0){
              await this.getInWorkList()
            }
      }
      catch( e) {
        this.errors=e.response.data;
      }
    },
    showModal() {
      this.modalVisible = true;
    },
    async executeNote(note) {
      note.status = 1
      this.inWorkList=this.inWorkList.filter(item => item.id !== note.id)
      await this.updateNote(note)
      if(this.executeList.length > 0){
        await this.getExecutionList()
      }

    },
    async cancelNote(note) {
      note.status = 2
      this.inWorkList=this.inWorkList.filter(item => item.id !== note.id);
      await this.updateNote(note)
      if(this.cancelList.length > 0){
        await this.getCancelList()
      }
    },
    async restoreNote(note) {
      note.status = 0
      this.cancelList=this.cancelList.filter(item => item.id !== note.id)
      await this.updateNote(note)
      if(this.inWorkList.length > 0){
        await this.getInWorkList()
      }
    },
    async deleteNote(note) {
      await axios.delete(`/api/notes/${note.id}`)
      this.cancelList=this.cancelList.filter(item => item.id !== note.id)
    },
    async getExecutionList() {
      const response = await axios.get('/api/notes/getall',{
        params: {
          Status: 1,
          Page: this.pageNumber,
          PageSize: 3
        }
      });
      this.executeList = response.data
    },
    decreasePage() {
      if(this.pageNumber>1)
        this.pageNumber--;
    },

    async getInWorkList() {
      const response = await axios.get('/api/notes/getall',{
        params: {
          Status: 0,
          Page: this.pageNumber,
          PageSize: 3
        }
      });
      this.inWorkList = response.data
    },
    async getCancelList() {
      const response = await axios.get('/api/notes/getall',{
        params: {
          Status: 2,
          Page: this.pageNumber,
          PageSize: 3
        }
      });
      this.cancelList = response.data
    },
    async updateNote(note) {
      await axios.patch('/api/notes/update', note)
    }
  }
}
</script>

<template>
  <div class="row rowBtn">
    <button class="btnMenu" @click="showModal">Новая задача</button>
    <button class="btnMenu" @click="getInWorkList">Задачи в работе</button>
    <button class="btnMenu" @click="getExecutionList">Выполненные задачи</button>
    <button class="btnMenu" @click="getCancelList">Отмененные задачи</button>
  </div>
  <div>
    <modal-window v-model:showModal="modalVisible">
      <note-form @create="createNote" :errors-array="errors" />
    </modal-window>
  </div>
  <div class="cR">
    <note-list :notes="inWorkList" @execute="executeNote" @cancel="cancelNote" />
    <note-list :notes="executeList" />
    <note-list :notes="cancelList" @restore="restoreNote"  @delete="deleteNote" />
  </div>
<div class="botRow">
  <div class="botCol">
    <button class="btnArrow" @click="decreasePage"> < </button>
    <span class="numb"> {{ pageNumber }} </span>
    <button CLASS="btnArrow" @click="()=>pageNumber++"> > </button>
  </div>
</div>
</template>

<style scoped>
header {
  line-height: 1.5;
}

@media (min-width: 1024px) {
}
body {
  /*background-image: url(/src/assets/bg.jpg);*/
  background:linear-gradient(rgba(192, 192, 192, 0.5), rgba(144, 238, 144, 0.4));
  background-size: auto;
  min-height: 100vh;
}
.btnMenu {
  margin: 10px;
  height: auto;
  width: 150px;
  border: groove 2px darkgreen;
  border-radius: 5px;
  padding: 20px 10px;
  box-shadow: 10px 5px 7px gray;
}
.rowBtn {
  display: flex;
  flex-direction: row;
  justify-content: center;
  border-bottom: solid 1px darkgreen;
}
button:hover {
  scale: 1.1;
}
.customCol {
  white-space: normal;
  display: inline-block;
  width: 30%;
  vertical-align: top;
}
.botRow {
  display: flex;
  flex-direction: row;
  justify-content: center;
}
.btnArrow {
  margin: 5px;
  color: green;

}
.botCol {
  position: absolute;
  bottom:0;
}
.numb {
  color: green;
}
</style>
