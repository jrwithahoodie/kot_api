CREATE TRIGGER [dbo].[T_update_game]
   ON  [dbo].[Games] 
   AFTER UPDATE
AS 
BEGIN
	
	declare @idEquipo1 int;
	declare @idEquipo2 int;

	declare @Score1 int;
	declare @Score2 int;

	declare @Score1Old int;
	declare @Score2Old int;

	declare @PuntosEquipo1 int = 0;
	declare @PuntosEquipo2 int = 0;

	declare @Win1 int = 0;
	declare @Win2 int = 0;

	declare @Defeat1 int = 0;
	declare @Defeat2 int = 0;

	declare @average int ;
	declare @Points_diff1 int = 0;
	declare @Points_diff2 int = 0;

	SELECT
	@idEquipo1 = [Team1Id],
	@idEquipo2 = [Team2Id],
	@Score1 = [Score1],
	@Score2 = [Score2],
	@Score1Old = [Score1Old],
	@Score2Old = [Score2Old]
	FROM inserted;

	-- DESHACER LAS ACTUALIZACIONES DEL PARTIDO
	BEGIN

		set @average = ISNULL(ABS(@Score1Old - @Score2Old),0);
		set @Points_diff1 = @average;
		set @Points_diff2 = @average;

		if (@Score1Old > @Score2Old) begin
			set @PuntosEquipo1 = 3;
			set @Win1 = 1;
			set @Defeat2 = 1;
			set @Points_diff2 = @Points_diff2 *  (-1);
		end;
		
		if (@Score2Old > @Score1Old) begin
			set @PuntosEquipo2 = 3;
			set @Win2 = 1;
			set @Defeat1 = 1;
			set @Points_diff1 = @Points_diff1 *  (-1);
		end;

		update Teams set 
			Classification_points = Classification_points - @PuntosEquipo1, 
			Wins = Wins - @Win1,
			Defeats = Defeats - @Defeat1,
			Points_diff = Points_diff - @Points_diff1
		where id = @idEquipo1;
		
		update Teams set Classification_points = Classification_points - @PuntosEquipo2,
			Wins = Wins - @Win2,
			Defeats = Defeats - @Defeat2,
			Points_diff = Points_diff - @Points_diff2
		where id = @idEquipo2;

	END;

	set @PuntosEquipo1 = 0;
	set @PuntosEquipo2 = 0;
	
	set @Win1 = 0;
	set @Win2 = 0;
	
	set @Defeat1 = 0;
	set @Defeat2 = 0;
	
	set @Points_diff1 = 0;
	set @Points_diff2 = 0;


	-- APLICAR LAS ACTUALIZACIONES DEL PARTIDO
	BEGIN

		set @average = ISNULL(ABS(@Score1 - @Score2),0);
		set @Points_diff1 = @average;
		set @Points_diff2 = @average;

		if (@Score1 > @Score2) begin
			set @PuntosEquipo1 = 3;
			set @Win1 = 1;
			set @Defeat2 = 1;
			set @Points_diff2 = @Points_diff2 * (-1);
		end;
		
		if (@Score2 > @Score1) begin
			set @PuntosEquipo2 = 3;
			set @Win2 = 1;
			set @Defeat1 = 1;
			set @Points_diff1 = @Points_diff1 *  (-1);
		end;

		update Teams set 
			Classification_points = Classification_points + @PuntosEquipo1, 
			Wins = Wins + @Win1,
			Defeats = Defeats + @Defeat1,
			Points_diff = Points_diff + @Points_diff1
		where id = @idEquipo1;
		
		update Teams set Classification_points = Classification_points + @PuntosEquipo2,
			Wins = Wins + @Win2,
			Defeats = Defeats + @Defeat2,
			Points_diff = Points_diff + @Points_diff2
		where id = @idEquipo2;

	END;
	   
END
