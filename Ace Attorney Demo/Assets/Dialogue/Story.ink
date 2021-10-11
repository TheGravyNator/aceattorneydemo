VAR previousKnot = -> Snack

# Plate
"The unwashed dishes."
-> Snack

=== Snack ===
# neutral CrossExamine
Maya: I was having a late night snack.
+ [Press]
    -> PressSnack
+ [PresentWrong]
    ~ previousKnot = -> Snack
    -> WrongEvidence
+ [Continue]
    -> AfterMeal

=== AfterMeal ===
# thinking CrossExamine
Maya: After the meal, I wanted to go to my room.
+ [Press]
    -> PressAfterMeal
+ [PresentWrong]
    ~ previousKnot = -> AfterMeal
    -> WrongEvidence
+ [Continue]
    -> Remembered

=== Remembered ===
# shocked CrossExamine
Maya: But then I remembered that I needed to wash my plate!
+ [Press]
    -> PressRemembered
+ [PresentWrong]
    ~ previousKnot = -> Remembered
    -> WrongEvidence
+ [Continue]
    -> Washing

=== Washing ====
# pose CrossExamine
Maya: So I rinsed my plate under the sink and put it back in the cupboard.
+ [Press]
    -> PressWashing
+ [PresentWrong]
    ~ previousKnot = -> Washing
    -> WrongEvidence
+ [Continue]
    -> CrossExaminationEnd
+ [PresentRight]
    -> RightEvidence

=== CrossExaminationEnd ===
# thinking
Phoenix: It doesn't sound like she's lying...
# thinking
Phoenix: ...But the evidence says something else.
# neutral
Phoenix: There must be a contradiction in there somewhere.
-> Snack

=== WrongEvidence ===
# pose
Franziska: Foolish fools make foolishly foolish mistakes, Phoenix Wright.
# shocked
Phoenix: I guess that wasn't the right choice.
-> previousKnot

=== RightEvidence ===
# slam
Phoenix: Hold it one second!
# pose
Phoenix: Are you sure about that, Maya?
# shocked
Maya: W-what?
# shocked
Franziska: W-what are you insinuating, Phoenix Wright?
-> END

=== PressSnack ===
# thinking
Phoenix: Wasn't it almost bed time?
# neutral
Phoenix: Doesn't your metabolism slow down, making you gain more weight?
# shocked
Maya: Wait, is that true?!
# neutral
Franziska: You would do better to refrain from badgering my witness, Phoenix Wright.
# shocked
Phoenix: What's with that menacing look?
-> AfterMeal

=== PressAfterMeal ===
# neutral
Phoenix: Do you remember what time it was?
# thinking
Maya: Well, it was somewhere after 8, because Steel Samurai had already ended.
# neutral
Franziska: Clearly this has no importance to the case.
# shocked
Phoenix: Why do you get to decide that...
-> Remembered

=== PressRemembered ===
# pose
Phoenix: Do you have any way to prove that?
# shocked
Maya: I-I do not...
# pose
Franziska: The responsibility of presenting evidence is yours to bear, Phoenix Wright.
# shocked 
Phoenix: Why do you keep referring to me with my full name...
-> Washing

=== PressWashing ===
# neutral
Phoenix: And you're certain of this fact?
# pose
Maya: I am completely sure!
# pose
Franziska: There is no way you can squirm yourself out of this one, Phoenix Wright!
# shocked
Phoenix: Did she need to use the word squirm...?
# thinking
Phoenix: Hmm, but that's weird. Didn't I see something in the Court Record that contradicts that statement?
-> CrossExaminationEnd