<template>
    <a class="button" @click="formExerciseOpen = !formExerciseOpen">Add an exercise</a>
    <div v-if="formExerciseOpen">
        <p>Nom de l'exercice</p>
        <input v-model="label" type="text" />
        <p>PR sur l'exercice</p>
        <input v-model="pr" type="number" />
        <p>Display position</p>
        <input v-model="displayPosition" type="number" />
        <p>Type de label</p>
        <select v-model="typeId">
            <option v-for="t in types" :key="t.id" :value="t.id">{{ t.label }}</option>
        </select>
        <p>Unité de mesure</p>
        <input v-model="unit" type="text" />

        <button @click="createExercise">Valider</button>
        <button @click="formExerciseOpen = !formExerciseOpen">Fermer</button>
    </div>
    <div v-for="e in exerciseFromType" :key="e.id">
        <router-link :to="`/exercise/detail/${e.id}`" class="typeBlock">
            <h1>{{ e.label }}</h1>
        </router-link>
        <button @click="editExerciseOpen = !editExerciseOpen">Edit</button>
        <div v-if="editExerciseOpen">
            <p>Nom de l'exercice</p>
            <input v-model="label" type="text" />
            <p>PR sur l'exercice</p>
            <input v-model="pr" type="number" />
            <p>Display position</p>
            <input v-model="displayPosition" type="number" />
            <p>Type de label</p>
            <select v-model="typeId">
                <option v-for="t in types" :key="t.id" :value="t.id">{{ t.label }}</option>
            </select>
            <p>Unité de mesure</p>
            <select v-model="unit" type="text">
                <option v-for="u in units" :key="u.id" :value="u.code"> {{ u.code }}</option>
            </select>
            <button @click="editExercise(e.id)">Valider</button>
            <button @click="editExerciseOpen = !editExerciseOpen">Fermer</button>
        </div>
    </div>
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.fetchData();
    },
    data() {
        return {
            exercises: [],
            types: [],
            formExerciseOpen: false,
            editExerciseOpen: false,
            label: '',
            pr: 1,
            userId: 1,
            displayPosition: 1,
            typeId: 1,
            unit: '',
        };
    },
    methods: {
        fetchData() {
            this.exercises = [];
            this.types = [];
            this.loading = true;

            fetch(`${this.$api}/exercisetype`)
                .then(r => r.json())
                .then(json => {
                    this.types = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/exercises`)
                .then(r => r.json())
                .then(json => {
                    this.exercises = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/units`)
                .then(r => r.json())
                .then(json => {
                    this.units = json;
                    this.loading = false;
                    return;
                });
        },
        createExercise() {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    label: this.label,
                    pr: this.pr,
                    displayPosition: this.displayPosition,
                    user: this.userId,
                    label: this.label,
                    typeId: this.typeId
                })
            };
            fetch(`${this.$api}/exercises`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("L'exercice a bien été crée") }
                    else { alert("L'exercice n'a pas été crée") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
        },
        editExercise(eId) {
            const requestOptions = {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    id: eId,
                    label: this.label,
                    pr: this.pr,
                    displayPosition: this.displayPosition,
                    user: this.userId,
                    label: this.label,
                    typeId: this.typeId
                })
            };
            fetch(`${this.$api}/exercises/${eId}`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("L'exercice a bien été modifié") }
                    else { alert("L'exercice n'a pas été modifié") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
        }
    },
    computed: {
        exerciseFromType() {
            return this.exercises.filter(e => e.typeId == this.$route.params.typeId)
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./ExercisesPage.scss');

template {
    margin: 0 10%;
}
</style>