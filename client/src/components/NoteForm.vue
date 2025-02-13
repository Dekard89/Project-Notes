
<template>
<form name="noteForm" method="post" v-on:submit.prevent >
  <input v-model="noteRequest.title" type="text" class="form-control" required
         placeholder="Введите название задачи"/>
  <div v-for="error in errorsArray">
    <div v-if="error.propertyName==='Title'" class="text-danger ">
      {{error.errorMessage}}
    </div>
  </div>
  <input v-model="noteRequest.description" type="text" class="form-control" required
         placeholder="Введите описание задачи">
  <div v-for="error in errorsArray">
    <div v-if="error.propertyName==='Description'" class="text-danger">
      {{error.errorMessage}}
    </div>
  </div>
  <button @click.prevent="createNote" class="btn-outline-primary" >OK</button>
</form>
</template>
<script>
import {timestamp} from "@antfu/utils";

export default {
  data() {
    return {
      noteRequest: {
        title: '',
        description: '',
        status: 0,
      }
    }
  },
  methods: {
    timestamp,
    createNote(){
      this.$emit('create',this.noteRequest)
      this.noteRequest= {
        title: '',
        description: '',

      }
    }
  },
  props: {
    errorsArray: {
      type: Array,
    }
  }
}
</script>

<style scoped>
input {
  border: 1px solid greenyellow;
  margin: 20px;
  align-self: center;
}
form {
  display: flex;
  flex-direction: column;
  border: 3px groove darkgreen;
  border-radius: 5%;
  padding: 20px;
}
button {
  height: 30px;
  width: 50px;
  border: groove 2px darkgreen;
  margin: 10px;
  align-self: center;
}
</style>