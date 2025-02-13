
<template>
  <div class="customCol">
    <note-component v-for="note in notes" :key="note.Id" :noteDto="note"
                    @execute="$emit('execute', note)"
                    @cancel="$emit('cancel', note)"
                    @restore="$emit('restore', note)"
                     @delete="$emit('delete', note)"/>
  </div>
</template>
<script>
import NoteComponent from "@/components/NoteComponent.vue";
export default {
  components:{
    NoteComponent
  },
  props:{
    notes:{
      type:Array,
      required:true
    }
  }
}
</script>

<style scoped>
.customCol{
  display: flex;
  flex-direction: column;
  flex-basis: 100%;
  height: 33%;
}
</style>